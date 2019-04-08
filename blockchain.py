import json
import os

from datetime import datetime
from hashlib import sha256, md5


class Blockchain(object):
    def __init__(self):
        self.chain = []
        self.pending_transactions = []

        # Create the genesis block
        print("Creating genesis block")

        self.new_block()

    def new_block(self, previous_hash=None, nonce=None):
        block = {
            'index': len(self.chain),
            'timestamp': datetime.utcnow().isoformat(),
            'transactions': self.pending_transactions,
            'previous_hash': previous_hash,
            'nonce': nonce,
        }

        # Get the hash of this new block, and add it to the block
        block["hash"] = self.hash(block)

        print(f"Created block {block['index']}")

        # Add the block to the chain
        self.chain.append(block)

        # Reset the list of pending transactions
        self.pending_transactions = []
        return block

    @staticmethod
    def hash(block):
        # Transform the block dict into a json string with sorted keys
        block_string = json.dumps(block, sort_keys=True).encode()

        # ... and hash it
        return sha256(block_string).hexdigest()

    def last_block(self):
        # Returns the last block in the chain (if there are blocks)
        return self.chain[-1] if self.chain else None

    @staticmethod
    def pow_is_acceptable(hash_of_block, difficulty):
        return hash_of_block[:difficulty] == "0" * difficulty

    @staticmethod
    def nonce():
        return sha256(os.urandom(16)).hexdigest()

    def proof_of_work(self, block=None, difficulty=4):
        block = block or self.last_block()

        while True:
            block["nonce"] = self.nonce()
            if self.pow_is_acceptable(hash_of_block=self.hash(block), difficulty=difficulty):
                print(f"Block hash is {self.hash(block)} with random string {block['nonce']}")
                return block
