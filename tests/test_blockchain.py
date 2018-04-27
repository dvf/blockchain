import hashlib
import json


# Test register nodes
def test_valid_nodes(blockchain_init):
    blockchain_init.register_node('http://192.168.0.1:5000')

    assert '192.168.0.1:5000' in blockchain_init.nodes


def test_malformed_nodes(blockchain_init):

    blockchain_init.register_node('http//192.168.0.1:5000')

    assert '192.168.0.1:5000' not in blockchain_init.nodes


def test_idempotency(blockchain_init):
    blockchain_init.register_node('http://192.168.0.1:5000')
    blockchain_init.register_node('http://192.168.0.1:5000')

    assert len(blockchain_init.nodes) == 1


# Test blocks and transactions
def test_block_creation(blockchain_init, create_block):
    latest_block = blockchain_init.last_block

    # The genesis block is create at initialization, so the length should be 2
    assert len(blockchain_init.chain) == 2
    assert latest_block['index'] == 2
    assert latest_block['timestamp'] is not None
    assert latest_block['proof'] == 123
    assert latest_block['previous_hash'] == 'abc'


def test_create_transaction(blockchain_init, create_transaction):
    transaction = blockchain_init.current_transactions[-1]

    assert transaction
    assert transaction['sender'] == 'a'
    assert transaction['recipient'] == 'b'
    assert transaction['amount'] == 1


def test_block_resets_transactions(blockchain_init, create_transaction):
    initial_length = len(blockchain_init.current_transactions)

    blockchain_init.new_block(proof=456, previous_hash='def')

    current_length = len(blockchain_init.current_transactions)

    assert initial_length == 1
    assert current_length == 0


def test_return_last_block(blockchain_init, create_block):
    created_block = blockchain_init.last_block

    assert len(blockchain_init.chain) == 2
    assert created_block is blockchain_init.chain[-1]


# Test hashing and proofs
def test_hash_is_correct(blockchain_init, create_block):
    new_block = blockchain_init.last_block
    new_block_json = json.dumps(blockchain_init.last_block, sort_keys=True).encode()
    new_hash = hashlib.sha256(new_block_json).hexdigest()

    assert len(new_hash) == 64
    assert new_hash == blockchain_init.hash(new_block)
