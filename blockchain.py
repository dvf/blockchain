import hashlib
import logging
from datetime import datetime

from database import Block, db


logger = logging.getLogger('root.blockchain')


class Blockchain:
    def __init__(self):
        self.current_transactions = []
        self.difficulty = 4

        # Create the genesis block if necessary
        if not self.get_blocks(0):
            self.new_block(previous_hash='1', proof=100, height=0)
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

    def new_block(self, proof, previous_hash, height):
        """
        Create a new Block in the Blockchain

        :param proof: The proof given by the Proof of Work algorithm
        :param previous_hash: Hash of previous Block
        :param height: The Height of the new block
        :return: New Block
        """

        block = Block(
            height=height,
            timestamp=datetime.now(),
            transactions=self.current_transactions,
            proof=proof,
            previous_hash=previous_hash,
        )

        db.add(block)
        db.commit()

        # Reset the current list of transactions
        self.current_transactions = []

        return block

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
        Creates a SHA-256 hash of a Block

        :param block: Block
        """

        block_bytes = block.to_json().encode()
        return hashlib.sha256(block_bytes).hexdigest()
