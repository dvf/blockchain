/**
 * Unit Tests
 *
 */


import assert from 'assert';

import Blockchain from '../lib/blockchain';
import helpers from '../lib/helpers';


// Holder for the tests
const unit = {};


// Fixtures
const block = {
  proof: 123,
  previousHash: 'abc',
};

const transaction = {
  sender: 'a',
  recipient: 'b',
  amount: 1,
};

const address = 'http://192.168.0.1:5000';


/**
 * Tests
 *
 */
unit[`Valid nodes should be registered`] = done =>  {
  const blockchain = new Blockchain();

  blockchain.registerNode(address);

  assert.ok([...blockchain.nodes].indexOf(address) > -1);
  done();
};

unit[`Invalid nodes should't be registered`] = done =>  {
    const blockchain = new Blockchain();
    const address = 'http//192.168.0.1:5000';

    blockchain.registerNode(address);

    assert.ok([...blockchain.nodes].indexOf(address) === -1);
    done();
};

unit[`Should not register already registered nodes`] = done =>  {
  const blockchain = new Blockchain();

  blockchain.registerNode(address);
  blockchain.registerNode(address);

  assert.ok([...blockchain.nodes].length === 1);
  done();
};

unit[`Block should be created and added to the chain`] = done =>  {
  const blockchain = new Blockchain();

  blockchain.newBlock(block);
  const latestBlock = blockchain.lastBlock;

  assert.ok([...blockchain.chain].length === 2);
  assert.ok(latestBlock.index === 2);
  assert.ok(latestBlock.timestamp);
  assert.ok(latestBlock.proof === 123);
  assert.ok(latestBlock.previousHash = 'abc');
  done();
};

unit[`Transaction should be created and added to the block`] = done =>  {
  const blockchain = new Blockchain();

  const transactionDetails = blockchain.newTransaction(transaction);
  const lastTransaction = blockchain.currentTransactions[transactionDetails.transactionId];

  assert.ok(lastTransaction);
  assert.ok(lastTransaction.sender === transaction.sender);
  assert.ok(lastTransaction.recipient === transaction.recipient);
  assert.ok(lastTransaction.amount === transaction.amount);
  done();
};

unit[`New blocks should not contain any transactions`] = done =>  {
  const blockchain = new Blockchain();

  blockchain.newTransaction(transaction);
  const initialLength = Object.keys(blockchain.currentTransactions).length;

  blockchain.newBlock(block);
  const currentLength = Object.keys(blockchain.currentTransactions).length;

  assert.ok(initialLength === 1);
  assert.ok(currentLength === 0);
  done();
};

unit[`Created block should be added to the chain and returned as the last block`] = done =>  {
  const blockchain = new Blockchain();

  blockchain.newBlock(block);
  const createdBlock = blockchain.lastBlock;


  assert.ok(blockchain.chain.length === 2);
  assert.ok(Object.is(createdBlock, blockchain.chain.slice(-1)[0]));
  done();
};

unit[`Hash should be valid`] = done =>  {
  const blockchain = new Blockchain();

  blockchain.newBlock(block);

  const newBlock = blockchain.lastBlock;
  const newBlockString = helpers.orderedJsonStringify(newBlock);
  const newHash = helpers.hash(newBlockString, newBlock.proof.toString());


  assert.ok(newHash.length === 64);
  assert.ok(newHash === Blockchain.hash(newBlock));
  done();
};

// Export
export default unit;
