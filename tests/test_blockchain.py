import hashlib
import json
from base64 import b64encode
from unittest import TestCase

import ecdsa
from blockchain import Blockchain
from transaction import Transaction


private_key = ecdsa.SigningKey.generate()
public_key = private_key.get_verifying_key()

private_key_2 = ecdsa.SigningKey.generate()
public_key_2 = private_key.get_verifying_key()


class BlockchainTestCase(TestCase):

    def setUp(self):
        self.blockchain = Blockchain()

    def create_block(self, proof=123, previous_hash='abc', miner=public_key.to_string()):
        self.blockchain.new_block(proof, previous_hash, miner)

    def create_transaction(
            self,
            sender=public_key.to_string(),
            recipient=public_key_2.to_string(),
            amount=1,
            sender_private_key=private_key
    ):
        transaction = Transaction(
            sender=sender,
            recipient=recipient,
            amount=amount,
        )
        transaction.create_signature(private_key=sender_private_key)
        self.blockchain.new_transaction(
            sender=transaction.sender,
            recipient=transaction.recipient,
            amount=transaction.amount,
            signature=transaction.signature
        )


class TestRegisterNodes(BlockchainTestCase):

    def test_valid_nodes(self):
        blockchain = Blockchain()

        blockchain.register_node('http://192.168.0.1:5000')

        self.assertIn('192.168.0.1:5000', blockchain.nodes)

    def test_malformed_nodes(self):
        blockchain = Blockchain()

        blockchain.register_node('http//192.168.0.1:5000')

        self.assertNotIn('192.168.0.1:5000', blockchain.nodes)

    def test_idempotency(self):
        blockchain = Blockchain()

        blockchain.register_node('http://192.168.0.1:5000')
        blockchain.register_node('http://192.168.0.1:5000')

        assert len(blockchain.nodes) == 1


class TestBlocksAndTransactions(BlockchainTestCase):

    def test_block_creation(self):
        self.create_block()

        latest_block = self.blockchain.last_block

        # The genesis block is create at initialization, so the length should be 2
        assert len(self.blockchain.chain) == 2
        assert latest_block['index'] == 2
        assert latest_block['timestamp'] is not None
        assert latest_block['proof'] == 123
        assert latest_block['previous_hash'] == 'abc'
        assert latest_block['miner'] == b64encode(public_key.to_string()).decode()

    def test_create_transaction(self):
        self.create_transaction()

        transaction = self.blockchain.current_transactions[-1]

        assert transaction
        assert transaction['sender'] == b64encode(public_key.to_string()).decode()
        assert transaction['recipient'] == b64encode(public_key_2.to_string()).decode()
        assert transaction['amount'] == 1

        assert Transaction.from_dict(
            transaction['sender'],
            transaction['recipient'],
            transaction['amount'],
            transaction['signature']
        ).is_signature_verified()

    def test_block_resets_transactions(self):
        self.create_transaction()

        initial_length = len(self.blockchain.current_transactions)

        self.create_block()

        current_length = len(self.blockchain.current_transactions)

        assert initial_length == 1
        assert current_length == 0

    def test_return_last_block(self):
        self.create_block()

        created_block = self.blockchain.last_block

        assert len(self.blockchain.chain) == 2
        assert created_block is self.blockchain.chain[-1]


class TestHashingAndProofs(BlockchainTestCase):

    def test_hash_is_correct(self):
        self.create_block()

        new_block = self.blockchain.last_block
        new_block_json = json.dumps(new_block, sort_keys=True).encode()
        new_hash = hashlib.sha256(new_block_json).hexdigest()

        assert len(new_hash) == 64
        assert new_hash == self.blockchain.hash(new_block)
