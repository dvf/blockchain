import SHA256 from 'crypto-js/sha256';
import parse from 'url-parse';
import fetch from 'node-fetch';

function getSHA256HexString(input) {
  return SHA256(input).toString();
}

class Blockchain {
  constructor() {
    this.chain = [];
    this.currentTransactions = [];
    this.nodes = new Set();

    // Create the genesis block
    this.newBlock(100, '1');
  }

  /**
   * Creates a SHA-256 hash of a Block
   * @param {object} block Block
   * @returns {string} Hash
   */
  hash(block) {
    // We must make sure that the attributes are ordered, or we'll have inconsistent hashes
    //
    const blockString = JSON.stringify(block, Object.keys(block).sort());

    // Return the hash in hex format
    return getSHA256HexString(blockString);
  }

  /**
   * Create a new Block in the Blockchain
   * @param {number} proof The proof given by the Proof of Work algorithm
   * @param {string} previousHash Hash of previous Block
   * @returns {object} New block
   */
  newBlock(proof, previousHash) {
    const block = {
      index: this.chain.length + 1,
      timestamp: Date.now(),
      transactions: this.currentTransactions,
      proof,
      previousHash: previousHash || this.hash(this.lastBlock()),
    };

    // Reset the current list of transactions
    this.currentTransactions = [];

    this.chain.push(block);

    return block;
  }

  /**
   * Creates a new transaction to go into the next mined Block
   * @param {string} sender Address of the Sender
   * @param {string} recipient Address of the Recipient
   * @param {string} amount Amount
   * @returns {number} The index of the Block that will hold this transaction
   */
  newTransaction(sender, recipient, amount) {
    this.currentTransactions.push({
      sender,
      recipient,
      amount
    });

    return this.chain.length + 1;
  }

  /**
   * Returns the last Block in the chain
   * @returns {object} The last Block in the chain
   */
  lastBlock() {
    return this.chain[this.chain.length - 1];
  }

  /**
   * Simple Proof of Work Algorithm:
   *  - Find a number p' such that hash(pp') contains leading 4 zeroes, where p is the previous p'
   *  - p is the previous proof, and p' is the new proof
   * @param {number} lastProof The previous proof
   * @returns {number} The new proof
   */
  proofOfWork(lastProof) {
    let proof = 0;

    while(!this.validProof(lastProof, proof)) {
      proof++;
    }

    return proof;
  }

  /**
   * Validates the Proof: Does hash(lastProof, proof) contain 4 leading zeroes?
   * @param {number} lastProof Previous Proof
   * @param {number} proof Current Proof
   * @returns {boolean} true if correct, false if not
   */
  validProof(lastProof, proof) {
    const guess = `${lastProof}${proof}`;
    const guessHash = getSHA256HexString(guess);

    return /^0000/.test(guessHash);
  }

  /**
   * Add a new node to the list of nodes
   * @param {string} address Address of node. Eg. 'http://192.168.0.5:5000'
   */
  registerNode(address) {
    const host = parse(address).host;

    this.nodes.add(host);
  }

  /**
   * Determine if a given blockchain is valid
   * @param {array} chain A blockchain
   * @returns {boolean} true if valid, false if not
   */
  validChain(chain) {
    let index = 1;

    while(index < chain.length) {
      const previousBlock = chain[index - 1];
      const block = chain[index];

      // Check that the hash of the block is correct
      if (block.previousHash !== this.hash(previousBlock)) {
        return false;
      }

      // Check that the Proof of Work is correct
      if (!this.validProof(previousBlock.proof, block.proof)) {
        return false;
      }

      index++;
    };

    return true;
  }

  /**
   * This is our Consensus Algorithm, it resolves conflicts
   * by replacing our chain with the longest one in the network.
   * @returns {Promise} Resolves with true if the chain is updated, else false
   */
  resolveConflicts() {
    const fetchPromises = [];

    this.nodes.forEach((host) => {
      fetchPromises.push(
        fetch(`http://${host}/chain`)
          .then(res => {
            if (res.ok) {
              return res.json();
            }
          })
          .then(json => json)
      );
    });

    return Promise.all(fetchPromises).then((chains) => {
      // We're only looking for chains longer than ours
      let newChain = null;
      let maxLength = this.chain.length;

      chains.forEach(({ chain }) => {
        // Check if the length is longer and the chain is valid
        if (chain.length > maxLength && this.validChain(chain)) {
          maxLength = chain.length;
          newChain = chain;
        }
      });

      // Replace our chain if we discovered a new, valid chain longer than ours
      if (newChain) {
        this.chain = newChain;
      }

      return !!newChain;
    });
  }
}

export default Blockchain;
