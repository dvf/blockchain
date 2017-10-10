from blockchain import app, Blockchain, node_identifier
import json


def test_get_genesis_chain(client):
    """Test get the genesis of chain."""
    rv = client.get('/chain')
    data = json.loads(rv.data)
    assert rv.status_code == 200
    assert 'chain' in data
    assert 'length' in data
    assert data['length'] == 1


def test_first_block_mined(client):
    """Test mining the first block."""
    rv = client.get('/mine')
    data = json.loads(rv.data)
    assert rv.status_code == 200
    assert 'message' in data
    assert 'index' in data
    assert 'transactions' in data
    assert 'proof' in data
    assert 'previous_hash' in data
    assert data['index'] == 2


def test_mining_blocks_increases_index(client):
    """Test mining blocks increases the index."""
    rv = client.get('/mine')
    data_first = json.loads(rv.data)
    rv = client.get('/mine')
    data_second = json.loads(rv.data)
    assert data_first['index'] == 2
    assert data_second['index'] == 3


def test_mining_blocks_rewards_miner(client):
    """Test mining blocks gives a fix reward to the miner - current node."""
    for _ in range(3):
        rv = client.get('/mine')
        data = json.loads(rv.data)
        assert len(data['transactions']) == 1
        last_transaction = data['transactions'][-1]
        assert last_transaction['sender'] == '0'
        assert last_transaction['recipient'] == node_identifier
        assert last_transaction['amount'] == 1


def test_add_new_transactions_in_one_block(client):
    """Test creating transactions in the next block."""
    for _ in range(3):
        rv = client.post('/transactions/new', data=json.dumps({
            'sender': 'X',
            'recipient': 'Y',
            'amount': 100.0
        }), content_type='application/json')
        assert rv.status_code == 201
        data = json.loads(rv.data)
        assert 'message' in data
        assert 'Block 2' in data['message']


def test_add_new_transactions_in_more_blocks(client):
    """Test creating one transaction in each of the next few blocks."""
    for i in range(1, 4):
        # New transaction
        rv = client.post('/transactions/new', data=json.dumps({
            'sender': 'X',
            'recipient': 'Y',
            'amount': 100.0
        }), content_type='application/json')
        assert rv.status_code == 201
        data = json.loads(rv.data)
        assert 'message' in data
        assert f'Block {i + 1}' in data['message']

        # New block
        rv = client.get('/mine')
        assert rv.status_code == 200
        data = json.loads(rv.data)
        assert len(data['transactions']) == 2  # 1 for transaction, 1 for reward
    

def test_register_nodes(client):
    """Test registering other nodes."""
    pass


def test_resolving_conflicts_between_nodes(client):
    """Test resolving conflicts with other registered nodes."""
    pass