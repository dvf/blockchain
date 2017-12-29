import logging
from datetime import datetime

from database import Block, db
from helpers import hash_block


logger = logging.getLogger('root.blockchain')


class Blockchain:
    def __init__(self):
        self.current_transactions = []
        self.difficulty = 4

        # Create the genesis block if it doesn't exist
        if not self.last_block:
            block = self.build_block()
            block['hash'] = hash_block(block)
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

            # Check that the hash_block of the block is correct
            if block['previous_hash'] != self.hash_block(last_block):
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
            'height': last_block.height + 1 if last_block else 0,
            'timestamp': datetime.utcnow(),
            'transactions': self.current_transactions,
            'previous_hash': last_block.hash if last_block else 0,
            'proof': -1,
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
