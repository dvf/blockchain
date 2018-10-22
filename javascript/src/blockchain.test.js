import SHA256 from 'crypto-js/sha256';
import Blockchain from './blockchain';

// Mock node-fetch
import fetch from 'node-fetch';
jest.mock('node-fetch', () => jest.fn());

describe('Blockchain', () => {
  let blockchain;

  beforeEach(() => {
    blockchain = new Blockchain();
  });

  describe('constructor', () => {
    it('should create a genesis block', () => {
      expect(blockchain.chain.length).toEqual(1);
      expect(blockchain.chain[0]).toMatchObject({
        index: 1,
        previousHash: '1',
        proof: 100,
        transactions: []
      });
    });
  });

  describe('hash', () => {
    it('should return a SHA256 hex string hash', () => {
      const objectToHash = { hej: 2 };
      const expectedHash = '630f4907b4e7d0f0a480aaccdf66c175249a04ba5dedc436ed6ac5957cf0080b';

      expect(blockchain.hash(objectToHash)).toEqual(expectedHash);
    });

    it('should return the same hash no matter of the order of the object attributes', () => {
      const object1 = { a: 1, b: 2 };
      const object2 = { b: 2, a: 1 };

      const hash1 = blockchain.hash(object1);
      const hash2 = blockchain.hash(object2);

      expect(hash1).toEqual(hash2);
    });
  });

  describe('newBlock', () => {
    it('should create a new block, add it to the chain and return it', () => {
      const lastBlock = blockchain.lastBlock();
      const lastProof = lastBlock.proof;
      const proof = blockchain.proofOfWork(lastProof);
      const previousHash = blockchain.hash(lastBlock);
      const block = blockchain.newBlock(proof, previousHash);

      expect(blockchain.chain.length).toEqual(2);
      expect(blockchain.chain[1]).toEqual(block);
      expect(blockchain.chain[1]).toMatchObject({
        index: 2,
        proof: 35293,
        transactions: []
      });
    });
  });

  describe('newTransaction', () => {
    it('should create a new transaction', () => {
      const expectedTransactions = [{
        amount: 200,
        recipient: 'recipient',
        sender: 'sender'
      }];

      blockchain.newTransaction('sender', 'recipient', 200);

      expect(blockchain.currentTransactions).toEqual(expectedTransactions);
    });

    it('should return the next index of the block that will hold the transaction', () => {
      const index = blockchain.newTransaction('sender', 'recipient', 200);

      expect(index).toEqual(blockchain.chain.length + 1);
    });
  });

  describe('lastBlock', () => {
    it('should return the last block in the chain', () => {
      const expectedLastBlock = blockchain.chain[blockchain.chain.length - 1];
      const lastBlock = blockchain.lastBlock();

      expect(lastBlock).toEqual(expectedLastBlock);
    });
  });

  describe('proofOfWork', () => {
    it('should return a new valid proof which is greater than the lastProof', () => {
      const lastProof = 100;

      const proof = blockchain.proofOfWork(lastProof);
      const valid = blockchain.validProof(lastProof, proof);

      expect(valid).toEqual(true);
      expect(proof).toBeGreaterThan(lastProof);
    });
  });

  describe('validProof', () => {
    it('should return true if the hash(lastProof, proof) contains 4 leading zeroes', () => {
      const lastProof = 100;
      const proof = 35293;

      const valid = blockchain.validProof(lastProof, proof);
      const guessHash = SHA256(`${lastProof}${proof}`).toString();

      expect(/^0000/.test(guessHash)).toEqual(true);
      expect(valid).toEqual(true);
    });

    it('should return false if the hash(lastProof, proof) does not contain 4 leading zeroes', () => {
      const lastProof = 100;
      const proof = 35292;

      const valid = blockchain.validProof(lastProof, proof);
      const guessHash = SHA256(`${lastProof}${proof}`).toString();

      expect(/^0000/.test(guessHash)).toEqual(false);
      expect(valid).toEqual(false);
    });
  });

  describe('registerNode', () => {
    it('should add the host and port of the node address to the nodes set', () => {
      const address = 'https://127.0.0.1:1234';
      const expectedNode = '127.0.0.1:1234';

      blockchain.registerNode(address);

      expect(blockchain.nodes.has(expectedNode)).toBeTruthy();
    });
  });

  describe('validChain', () => {
    it('should return false if the hash of a block is incorrect', () => {
      const chainWithIncorrectHash = [{
        index: 1,
        timestamp: 1515529759188,
        transactions: [],
        proof: 100,
        previousHash: '1'
      }, {
        index: 2,
        timestamp: 1515529769878,
        transactions: [{
          sender: '0',
          recipient: '49b04560365e490e8710c63acf9eb65f',
          amount: 1
        }],
        proof: 35293,
        previousHash: '---- INCORRECT HASH ----'
      }];

      const valid = blockchain.validChain(chainWithIncorrectHash);

      expect(valid).toEqual(false);
    });

    it('should return false if a proof of work is invalid', () => {
      const chainWithInvalidProofOfWork = [{
        index: 1,
        timestamp: 1515529759188,
        transactions: [],
        proof: 100,
        previousHash: '1'
      }, {
        index: 2,
        timestamp: 1515529769878,
        transactions: [{
          sender: '0',
          recipient: '49b04560365e490e8710c63acf9eb65f',
          amount: 1
        }],
        proof: 1, // Invalid proof
        previousHash: '866e4aa556c49e0b3d5ca2a9cb7406e0b562740a222d8e1e7660f8bb7b74ef90'
      }];

      const valid = blockchain.validChain(chainWithInvalidProofOfWork);

      expect(valid).toEqual(false);
    });

    it('should return true if all hashes and proof of work are coorect and valid', () => {
      const validChain = getValidChain();

      const valid = blockchain.validChain(validChain);

      expect(valid).toEqual(true);
    });
  });

  describe('resolveConflicts', () => {
    beforeEach(() => {
      blockchain.registerNode('http://127.0.0.1:1234');
    });

    it('should replace our chain and return true if a longer valid chain is found', () => {
      fetch.mockReturnValue(Promise.resolve({
        ok: true,
        json: () => ({
          chain: getValidChain()
        })
      }));

      expect(blockchain.chain.length).toEqual(1);

      return blockchain.resolveConflicts()
        .then((replaced) => {
          expect(replaced).toEqual(true);
          expect(blockchain.chain.length).toEqual(2);
        });
    });

    it('should keep our chain and return false if a longer valid chain is not found', () => {
      fetch.mockReturnValue(Promise.resolve({
        ok: true,
        json: () => ({
          chain: []
        })
      }));

      return blockchain.resolveConflicts()
        .then((replaced) => {
          expect(replaced).toEqual(false);
          expect(blockchain.chain.length).toEqual(1);
        });
    });
  });
});

function getValidChain() {
  return [{
    index: 1,
    timestamp: 1515529759188,
    transactions: [],
    proof: 100,
    previousHash: '1'
  }, {
    index: 2,
    timestamp: 1515529769878,
    transactions: [{
      sender: '0',
      recipient: '49b04560365e490e8710c63acf9eb65f',
      amount: 1
    }],
    proof: 35293,
    previousHash: '866e4aa556c49e0b3d5ca2a9cb7406e0b562740a222d8e1e7660f8bb7b74ef90'
  }];
}
