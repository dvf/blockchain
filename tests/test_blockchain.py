from blockchain import Blockchain


def test_genesis_block_chain():
    bc = Blockchain()
    assert bc.current_transactions == []
    assert bc.nodes == set()
    assert len(bc.chain) == 1
    last_block = bc.chain[-1]
    assert last_block['index'] == 1
    assert last_block['transactions'] == []
    assert last_block['proof'] == 100
    assert last_block['previous_hash'] == 1


def test_hash_block():
    bc = Blockchain()
