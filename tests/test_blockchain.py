import pytest

from blockchain import Blockchain


@pytest.fixture
def blockchain():
    return Blockchain()


class TestBlockchain:
    def test_genesis_block(self, blockchain):
        genesis_block = blockchain.chain[0]

        assert len(blockchain.chain) == 1
        assert genesis_block["index"] == 0
        assert genesis_block["transactions"] == []
        assert genesis_block["timestamp"]
        assert genesis_block["previous_hash"] is None
        assert genesis_block["nonce"] is None
        assert genesis_block["hash"]

    def test_pending_transactions_reset_after_block_addition(self, blockchain):
        blockchain.pending_transactions.append({"from": "Alice", "to": "Bob", "amount": 12})
        blockchain.new_block()

        assert blockchain.pending_transactions == []

    def test_blocks_are_hashed_correctly(self, blockchain):
        some_block = blockchain.new_block(previous_hash="abc123", nonce="abc123")

        assert some_block["hash"] == blockchain.hash({k: v for k, v in some_block.items() if k != "hash"})

    def test_blockchain_returns_last_block(self, blockchain):
        some_block = blockchain.new_block(previous_hash="abc123", nonce="abc123")

        assert blockchain.last_block() == some_block

    def test_pow_is_acceptable(self, blockchain):
        assert blockchain.pow_is_acceptable("00000acbdef123123987hsdkfjskdf213", 5) is True
        assert blockchain.pow_is_acceptable("0000acbdef123123987hsdkfjskdf2134", 5) is False

    def test_nonce(self, blockchain):
        assert blockchain.nonce()
        assert blockchain.nonce() != blockchain.nonce()

    def test_proof_of_work(self, blockchain):
        mined_block = blockchain.proof_of_work(difficulty=3)

        assert mined_block["nonce"]
        assert blockchain.hash(mined_block)[:3] == "000"
