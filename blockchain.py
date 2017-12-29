import json
import logging
from datetime import datetime
from hashlib import sha256

from database import Block, db, DateTimeEncoder
from helpers import json_serializer


logger = logging.getLogger('root.blockchain')


class Blockchain:
    def __init__(self):
        self.current_transactions = []
        self.difficulty = 4
        self.current_block = None

        # Create the genesis block if necessary
        if not self.last_block:
            block = self.build_block()
            block['hash'] = self.hash(block)
            self.save_block(block)

            logger.info("✨ Created genesis block")

        logger.info("Blockchain Initiated")

    @staticmethod
    def get_blocks(height=0):
        """
        Returns all blocks from a given height

        :param height: <int> The height from which to return blocks
        :return:
        """

        blocks = db.query(Block).filter(Block.height >= height).all()
        return [block.to_dict() for block in blocks]

    def valid_chain(self, chain):
        """
        Determines if a given blockchain is valid

        :param chain: A blockchain
        :return: True if valid, False if not
        """

        last_block = chain[0]
        current_index = 1

        while current_index < len(chain):
            block = chain[current_index]

            # Check that the hash of the block is correct
            if block['previous_hash'] != self.hash(last_block):
                return False

            # Check that the Proof of Work is correct
            if not self.valid_proof(last_block['proof'], block['proof']):
                return False

            last_block = block
            current_index += 1

        return True

    def build_block(self):
        """
        Create a new Block in the Blockchain
        """
        last_block = self.last_block

        return {
            'transactions': self.current_transactions,
            'previous_hash': last_block.hash if last_block else 0,
            'timestamp': datetime.now()
        }

    def save_block(self, block_dict):
        """
        Saves Block to the database and resets outstanding transactions

        :param block_dict: A dictionary representation of a Block
        :return:
        """
        block = Block(**block_dict)
        db.add(block)
        db.commit()

        # Reset the current list of transactions
        self.current_transactions = []

    def new_transaction(self, sender, recipient, amount):
        """
        Creates a new transaction to go into the next mined Block

        :param sender: Address of the Sender
        :param recipient: Address of the Recipient
        :param amount: Amount
        :return: The index of the Block that will hold this transaction
        """
        self.current_transactions.append({
            'sender': sender,
            'recipient': recipient,
            'amount': amount,
        })

        return self.last_block['index'] + 1

    @property
    def last_block(self):
        """
        Returns the last block in the Blockchain (greatest height)

        :return: <Block>
        """
        return db.query(Block).order_by(Block.height.desc()).first()

    @staticmethod
    def hash(block):
        """
        Creates a SHA-256 hash of the fields for a Block
        """
        json_string = json.dumps(block, sort_keys=True, cls=DateTimeEncoder)
        return sha256(json_string.encode()).hexdigest()
