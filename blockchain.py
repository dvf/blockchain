import hashlib
from datetime import datetime

from database import Block, db


class Blockchain:
    def __init__(self):
        self.current_transactions = []
        self.difficulty = 4

        # Create the genesis block with a height of 0
        self.new_block(previous_hash='1', proof=100, height=0)

    def get_blocks(self, height=0):
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

        block_string = block.to_json().encode()
        return hashlib.sha256(block_string).hexdigest()

    def proof_of_work(self, last_proof):
        """
        Simple Proof of Work Algorithm:
         - Find a number p' such that hash(pp') contains leading 4 zeroes, where p is the previous p'
         - p is the previous proof, and p' is the new proof

        :param last_proof: The proof from the previous block
        """

        proof = 0
        while self.valid_proof(last_proof, proof) is False:
            proof += 1

        return proof

    def valid_proof(self, last_proof, proof):
        """
        Validates the Proof

        :param last_proof: Previous Proof
        :param proof: Current Proof
        :return: True if correct, False if not.
        """

        guess = f'{last_proof}{proof}'.encode()
        guess_hash = hashlib.sha256(guess).hexdigest()

        return guess_hash[:self.difficulty] == '0' * self.difficulty  # In Python, '0' * 4 gives '0000'


# @app.route('/mine', methods=['GET'])
# def mine():
#     # We run the proof of work algorithm to get the next proof...
#     last_block = blockchain.last_block
#     last_proof = last_block['proof']
#     proof = blockchain.proof_of_work(last_proof)
#
#     # We must receive a reward for finding the proof.
#     # The sender is "0" to signify that this node has mined a new coin.
#     blockchain.new_transaction(
#         sender="0",
#         recipient=node_identifier,
#         amount=1,
#     )
#
#     # Forge the new Block by adding it to the chain
#     previous_hash = blockchain.hash(last_block)
#     block = blockchain.new_block(proof, previous_hash)
#
#     response = {
#         'message': "New Block Forged",
#         'index': block['index'],
#         'transactions': block['transactions'],
#         'proof': block['proof'],
#         'previous_hash': block['previous_hash'],
#     }
#     return jsonify(response), 200


# @app.route('/transactions/new', methods=['POST'])
# def new_transaction():
#     values = request.get_json()
#
#     # Check that the required fields are in the POST'ed data
#     required = ['sender', 'recipient', 'amount']
#     if not all(k in values for k in required):
#         return 'Missing values', 400
#
#     # Create a new Transaction
#     index = blockchain.new_transaction(values['sender'], values['recipient'], values['amount'])
#
#     response = {'message': f'Transaction will be added to Block {index}'}
#     return jsonify(response), 201
#
#
# @app.route('/chain', methods=['GET'])
# def full_chain():
#     response = {
#         'chain': blockchain.chain,
#         'length': len(blockchain.chain),
#     }
#     return jsonify(response), 200
#
#
# @app.route('/nodes/register', methods=['POST'])
# def register_nodes():
#     values = request.get_json()
#
#     nodes = values.get('nodes')
#     if nodes is None:
#         return "Error: Please supply a valid list of nodes", 400
#
#     for node in nodes:
#         blockchain.register_node(node)
#
#     response = {
#         'message': 'New nodes have been added',
#         'total_nodes': list(blockchain.nodes),
#     }
#     return jsonify(response), 201
#
#
# @app.route('/nodes/resolve', methods=['GET'])
# def consensus():
#     replaced = blockchain.resolve_conflicts()
#
#     if replaced:
#         response = {
#             'message': 'Our chain was replaced',
#             'new_chain': blockchain.chain
#         }
#     else:
#         response = {
#             'message': 'Our chain is authoritative',
#             'chain': blockchain.chain
#         }
#
#     return jsonify(response), 200
#
