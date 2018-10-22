import getApp from './app';
import request from 'supertest';
import Blockchain from './blockchain';

jest.spyOn(Date, 'now').mockImplementation(() => 1516811491833);
jest.mock('./utils', () => ({
  getNodeIdentifier: jest.fn().mockReturnValue('a4b08052903e4d87b43028955b8db6b4')
}));

describe('app', () => {
  let app;
  let blockchain;

  beforeEach(() => {
    blockchain = new Blockchain();
    app = getApp(blockchain);
  });

  afterAll(() => {
    jest.restoreAllMocks();
    jest.unmock('./utils');
  });

  it('should throw Error if a blockchain is not sent in getApp', () => {
    const testFn = () => {
      getApp();
    };

    expect(testFn).toThrow('You must send a blockchain');
  });

  describe('/mine', () => {
    it('should respond with status 200 and the new block along with a message', () => {
      return request(app)
        .get('/mine')
        .expect('Content-Type', /json/)
        .expect(200)
        .expect(function(res) {
          expect(res.body).toEqual({
            message: 'New Block Forged',
            index: 2,
            timestamp: 1516811491833,
            transactions:
            [{
              sender: '0',
              recipient: 'a4b08052903e4d87b43028955b8db6b4',
              amount: 1
            }],
            proof: 35293,
            previousHash: '18c0798e255811419eec3ad9209cb9a24d3adfea6d3ca89c8f816a26b01d918c'
          });
        });
    });
  });

  describe('/transactions/new', () => {
    const testMissingValues = (request) => {
      return request
        .expect('Content-Type', /text\/html/)
        .expect(400)
        .expect(res => {
          expect(res.text).toEqual('Missing values');
        });
    };
    it('should respond with status 400 and correct message if missing sender, recipient and amount', () => {
      return testMissingValues(
        request(app)
          .post('/transactions/new')
      );
    });

    it('should respond with status 400 and correct message if missing sender', () => {
      return testMissingValues(
        request(app)
          .post('/transactions/new')
          .field('recipient', 'recipient')
          .field('amount', '10')
      );
    });

    it('should respond with status 400 and correct message if missing recipient', () => {
      return testMissingValues(
        request(app)
          .post('/transactions/new')
          .field('amount', '10')
          .field('sender', 'sender')
      );
    });

    it('should respond with status 400 and correct message if missing amount', () => {
      return testMissingValues(
        request(app)
          .post('/transactions/new')
          .field('recipient', 'recipient')
          .field('sender', 'sender')
      );
    });

    describe('when posting sender, recipient and amount', () => {
      let req;
      let newTransactionSpy;

      beforeEach(() => {
        newTransactionSpy = jest.spyOn(blockchain, 'newTransaction');

        req = request(app)
          .post('/transactions/new')
          .field('recipient', 'recipient')
          .field('sender', 'sender')
          .field('amount', '10');
      });

      afterEach(() => {
        newTransactionSpy.mockRestore();
      });

      it('should return status 201 and correct data', () => {
        return req
          .expect('Content-Type', /json/)
          .expect(201)
          .expect(res => {
            expect(res.body).toEqual({
              message: 'Transaction will be added to Block 2'
            });
          });
      });

      it('should have called blockchain.newTransaction with correct parameters', () => {
        return req
          .expect(() => {
            expect(newTransactionSpy).toHaveBeenCalledWith('sender', 'recipient', '10');
          });
      });
    });
  });

  describe('/chain', () => {
    it('shold respond with status 200 and correct data', () => {
      return request(app)
        .get('/chain')
        .expect('Content-Type', /json/)
        .expect(200)
        .expect(res => {
          expect(res.body).toEqual({
            chain: blockchain.chain,
            length: blockchain.chain.length
          });
        });
    });
  });

  describe('/nodes/register', () => {
    it('should respond with status 400 and correct message if no nodes are sent', () => {
      return request(app)
        .post('/nodes/register')
        .expect('Content-Type', /text\/html/)
        .expect(400)
        .expect(res => {
          expect(res.text).toEqual('Error: Please supply a valid list of nodes');
        });
    });

    describe('when nodes are sent', () => {
      let registerNodeSpy;
      let req;
      const nodes = [
        'http://127.0.0.1:1111',
        'http://127.0.0.1:2222'
      ];

      beforeEach(() => {
        registerNodeSpy = jest.spyOn(blockchain, 'registerNode');

        req = request(app)
          .post('/nodes/register')
          .field('nodes', nodes);
      });

      afterEach(() => {
        registerNodeSpy.mockRestore();
      });

      it('should respond with status 201 and correct data', () => {
        return req
          .expect('Content-Type', /json/)
          .expect(201)
          .expect(res => {
            expect(res.body).toEqual({
              message: 'New nodes have been added',
              totalNodes: blockchain.nodes.length
            });
          });
      });

      it('should call registerNode for each node sent', () => {
        return req
          .expect(() => {
            nodes.forEach((node) => {
              expect(registerNodeSpy).toHaveBeenCalledWith(node);
            });
            expect(registerNodeSpy).toHaveBeenCalledTimes(2);
          });
      });
    });
  });

  describe('/nodes/resolve', () => {
    let resolveConflictsSpy;

    beforeEach(() => {
      resolveConflictsSpy = jest.spyOn(blockchain, 'resolveConflicts')
        .mockImplementation(() => Promise.resolve(true));
    });

    afterEach(() => {
      resolveConflictsSpy.mockRestore();
    });

    it('should call blockchain.resolveConflicts', () => {
      return request(app)
        .get('/nodes/resolve')
        .expect(() => {
          expect(resolveConflictsSpy).toHaveBeenCalledTimes(1);
        });
    });

    describe('when the chain has not been replaced', () => {
      beforeEach(() => {
        resolveConflictsSpy.mockReturnValue(Promise.resolve(false));
      });

      it('should respond with status 200 and correct data', () => {
        return request(app)
          .get('/nodes/resolve')
          .expect('Content-Type', /json/)
          .expect(200)
          .expect(res => {
            expect(res.body).toEqual({
              message: 'Our chain is authoritative',
              chain: blockchain.chain
            });
          });
      });
    });

    describe('when the chain has been replaced', () => {
      beforeEach(() => {
        resolveConflictsSpy.mockReturnValue(Promise.resolve(true));
      });

      it('should respond with status 200 and correct data', () => {
        return request(app)
          .get('/nodes/resolve')
          .expect('Content-Type', /json/)
          .expect(200)
          .expect(res => {
            expect(res.body).toEqual({
              message: 'Our chain was replaced',
              newChain: blockchain.chain
            });
          });
      });
    });

    describe('when blockchain.resolveConflicts rejects', () => {
      beforeEach(() => {
        resolveConflictsSpy.mockImplementation(() => Promise.reject());
      });

      it('should respond with status 502 and correct message', () => {
        return request(app)
          .get('/nodes/resolve')
          .expect('Content-Type', /text\/html/)
          .expect(502)
          .expect(res => {
            expect(res.text).toEqual('Failed to contact nodes');
          });
      });
    });
  });
});
