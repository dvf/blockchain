from unittest import TestCase
import blockchain
from blockchain import node_identifier
import json
import itertools


class TestRoutes(TestCase):
    def setUp(self):
        """""Define the setup app that uses a new blockchain in every test method."""
        _app = blockchain.app
        blockchain.blockchain = blockchain.Blockchain()
        self.app = _app.test_client()

    def test_get_genesis_chain(self):
        response = self.app.get('/chain')

        data = json.loads(response.data)
        assert response.status_code == 200
        assert 'chain' in data
        assert 'length' in data
        assert data['length'] == 1


    def test_first_block_mined(self):
        response = self.app.get('/mine')

        data = json.loads(response.data)
        assert response.status_code == 200
        assert 'message' in data
        assert 'index' in data
        assert 'transactions' in data
        assert 'proof' in data
        assert 'previous_hash' in data
        assert data['index'] == 2


    def test_mining_blocks_increases_index(self):
        response = self.app.get('/mine')
        data_first = json.loads(response.data)
        response = self.app.get('/mine')
        data_second = json.loads(response.data)
        assert data_first['index'] == 2
        assert data_second['index'] == 3

    def test_mining_blocks_rewards_miner(self):
        for _ in range(3):
            response = self.app.get('/mine')
            data = json.loads(response.data)
            assert len(data['transactions']) == 1
            last_transaction = data['transactions'][-1]
            assert last_transaction['sender'] == '0'
            assert last_transaction['recipient'] == node_identifier
            assert last_transaction['amount'] == 1

    def test_add_new_transactions_in_one_block(self):
        for _ in range(3):
            response = self.app.post('/transactions/new', data=json.dumps({
                'sender': 'X',
                'recipient': 'Y',
                'amount': 100.0
            }), content_type='application/json')
            assert response.status_code == 201
            data = json.loads(response.data)
            assert 'message' in data
            assert 'Block 2' in data['message']

    def test_add_new_transactions_in_more_blocks(self):
        for i in range(1, 4):
            # New transaction
            response = self.app.post('/transactions/new', data=json.dumps({
                'sender': 'X',
                'recipient': 'Y',
                'amount': 100.0
            }), content_type='application/json')
            assert response.status_code == 201
            data = json.loads(response.data)
            assert 'message' in data
            assert f'Block {i + 1}' in data['message']

            # New block
            response = self.app.get('/mine')
            assert response.status_code == 200
            data = json.loads(response.data)
            # 1 for transaction, 1 for reward
            assert len(data['transactions']) == 2

    def test_adding_incomplete_new_transaction(self):
        complete_data = (('sender', 'x'), ('recipient', 'y'), ('amount', 100.0))
        for missing_data in itertools.combinations(complete_data, len(complete_data) - 1):
            response = self.app.post(
                '/transactions/new',
                data=json.dumps(dict(missing_data)),
                content_type='application/json')
            assert response.status_code == 400

    def test_register_nodes(self):
        response = self.app.post('/nodes/register', data=json.dumps({
            'nodes': ['http://localhost:5001'],
        }), content_type='application/json')
        assert response.status_code == 201
        data = json.loads(response.data)
        assert 'message' in data
        assert len(data['total_nodes']) == 1
        assert 'localhost:5001' in data['total_nodes'][0]


    def test_register_invalid_nodes(self):
        response = self.app.post('/nodes/register', data=json.dumps({
            'wrong_key': None
        }), content_type='application/json')
        assert response.status_code == 400

    def test_no_nodes_to_resolve_conflict(self):
        response = self.app.get('/nodes/resolve')
        assert response.status_code == 200
        data = json.loads(response.data)
        assert data['message'] == 'Our chain is authoritative'
        assert 'chain' in data

    def test_response_of_mocking_resolving_conflicts_between_nodes(self):
        # Mokeypatch blockchain's method
        blockchain.blockchain.resolve_conflicts = lambda: True
        response = self.app.get('/nodes/resolve')
        assert response.status_code == 200
        data = json.loads(response.data)
        assert data['message'] == 'Our chain was replaced'
        assert 'new_chain' in data
