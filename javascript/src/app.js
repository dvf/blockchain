import express from 'express';
import bodyParser from 'body-parser';
import multer from 'multer';
import { getNodeIdentifier } from './utils';

const getApp = (blockchain) => {
  if (!blockchain) {
    throw new Error('You must send a blockchain');
  }

  // Generate a globally unique address for this node
  const nodeIdentifier = getNodeIdentifier();

  const app = express();

  const upload = multer(); // for parsing multipart/form-data
  app.use(bodyParser.json()); // for parsing application/json
  app.use(bodyParser.urlencoded({ extended: true })); // for parsing application/x-www-form-urlencoded

  app.get('/mine', (req, res) => {
    // We run the proof of work algorithm to get the next proof...
    const lastBlock = blockchain.lastBlock();
    const lastProof = lastBlock.proof;
    const proof = blockchain.proofOfWork(lastProof);

    // We must receive a reward for finding the proof.
    // The sender is "0" to signify that this node has mined a new coin.
    blockchain.newTransaction('0', nodeIdentifier, 1);

    // Forge the new Block by adding it to the chain
    const previousHash = blockchain.hash(lastBlock);
    const block = blockchain.newBlock(proof, previousHash);

    const response = {
      message: 'New Block Forged',
      ...block
    };

    res.send(response);
  });

  app.post('/transactions/new', upload.array(), (req, res) => {
    const { sender, recipient, amount } = req.body || {};

    // Check that the required fields are in the POST'ed data
    if (!(sender && recipient && amount)) {
      res.status(400).send('Missing values');
      return;
    }

    // Create a new Transaction
    const index = blockchain.newTransaction(sender, recipient, amount);

    const response = {
      message: `Transaction will be added to Block ${index}`
    };

    res.status(201).send(response);
  });

  app.get('/chain', (req, res) => {
    const response = {
      chain: blockchain.chain,
      length: blockchain.chain.length
    };

    res.send(response);
  });

  app.post('/nodes/register', upload.array(), (req, res) => {
    const { nodes } = req.body || [];

    if (!nodes) {
      res.status(400).send('Error: Please supply a valid list of nodes');
      return;
    }

    nodes.forEach((node) => {
      blockchain.registerNode(node);
    });

    const response = {
      message: 'New nodes have been added',
      totalNodes: blockchain.nodes.length
    };

    res.status(201).send(response);
  });

  app.get('/nodes/resolve', (req, res) => {
    let response;
    blockchain.resolveConflicts()
      .then((replaced) => {
        if (replaced) {
          response = {
            message: 'Our chain was replaced',
            newChain: blockchain.chain
          };
        } else {
          response = {
            message: 'Our chain is authoritative',
            chain: blockchain.chain
          };
        }

        res.send(response);
      })
      .catch(() => {
        res.status(502).send('Failed to contact nodes');
      });
  });

  return app;
};

export default getApp;
