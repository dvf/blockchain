import hashlib
import json
import time
from urllib.parse import urlparse
from uuid import uuid4
import urllib.request
import requests
from flask import Flask, jsonify, request


class Blockchain:
    def __init__(self):
        self.current_transactions = []
        self.chain = []
        self.nodes = set()
        self.difficulty=6
        self.blocktime = 10
        self.blockreward=0.01
        self.last_time=time.time()

        # Create the genesis block
        self.new_block(previous_hash='1', proof=100)

    def register_node(self, address):
        """
        Add a new node to the list of nodes

        :param address: Address of node. Eg. 'http://192.168.0.5:5000'
        """

        parsed_url = urlparse(address)
        if parsed_url.netloc:
            self.nodes.add(parsed_url.netloc)
        elif parsed_url.path:
            # Accepts an URL without scheme like '192.168.0.5:5000'.
            self.nodes.add(parsed_url.path)
        else:
            raise ValueError('Invalid URL')


    def valid_chain(self, chain):
        """
        Determine if a given blockchain is valid

        :param chain: A blockchain
        :return: True if valid, False if not
        """

        last_block = chain[0]
        current_index = 1
        while current_index < len(chain):
            block = chain[current_index]
            # Check that the hash of the block is correct
            t=self.hash(last_block)
            if block['previous_hash'] != t:
                print("here "+ current_index)
                return False
            x=self.valid_proof(last_block['proof'], block['proof'], block['previous_hash'])
            # Check that the Proof of Work is correct
            if not x :
                print("here2 " + current_index)
                return False

            last_block = block
            current_index += 1
        print("heeeeeere")
        return True

    @property
    def resolve_conflicts(self):
        """
        This is our consensus algorithm, it resolves conflicts
        by replacing our chain with the longest one in the network.

        :return: True if our chain was replaced, False if not
        """

        neighbours = self.nodes
        new_chain = None

        # We're only looking for chains longer than ours
        max_length = len(self.chain)

        # Grab and verify the chains from all the nodes in our network
        for node in neighbours:

            y = urllib.request.urlopen(f'http://{node}/chain').read()
            my_json = y.decode('utf8').replace("'", '"')
            data = json.loads(my_json)
            respons =json.dumps(data)
            length = data['length']
            chain = data['chain']

            print(f'{str(length)}" "+  {str(max_length)}')

            # Check if the length is longer and the chain is valid
            if length > max_length and self.valid_chain(chain):
                print("lol")
                max_length = length
                new_chain = chain

            # Replace our chain if we discovered a new, valid chain longer than ours
            if new_chain:
                self.chain = new_chain
                return True
        return False


    def new_block(self, proof, previous_hash):
        """
        Create a new Block in the Blockchain

        :param proof: The proof given by the Proof of Work algorithm
        :param previous_hash: Hash of previous Block
        :return: New Block
        """

        block = {
            'index': len(self.chain) + 1,
            'timestamp': time.time(),
            'transactions': self.current_transactions,
            'proof': proof,
            'previous_hash': previous_hash or self.hash(self.chain[-1]),
        }

        # Reset the current list of transactions
        self.current_transactions = []

        self.chain.append(block)
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
        return self.chain[-1]

    @staticmethod
    def hash(block):
        """
        Creates a SHA-256 hash of a Block

        :param block: Block
        """

        # We must make sure that the Dictionary is Ordered, or we'll have inconsistent hashes
        block_string = json.dumps(block, sort_keys=True).encode()
        return hashlib.sha256(block_string).hexdigest()

    def proof_of_work(self, last_block):
        """
        Simple Proof of Work Algorithm:

         - Find a number p' such that hash(pp') contains leading 4 zeroes
         - Where p is the previous proof, and p' is the new proof
         
        :param last_block: <dict> last Block
        :return: <int>
        """

        last_proof = last_block['proof']
        last_hash = self.hash(last_block)

        proof = 0
        while self.valid_proof(last_proof, proof, last_hash) is False:
            proof += 1

        return proof
    
    def getworkt(self):
        last_block = blockchain.last_block
        last_id=len(self.chain)+1
        last_proof = last_block['proof']
        last_hash = self.hash(last_block)
        self.last_time=time.time()
        return (last_id,last_hash,last_proof)


    @staticmethod
    def valid_proof(last_proof, proof, last_hash):
        """
        Validates the Proof

        :param last_proof: <int> Previous Proof
        :param proof: <int> Current Proof
        :param last_hash: <str> The hash of the Previous Block
        :return: <bool> True if correct, False if not.

        """

        guess = f'{last_proof}{proof}{last_hash}'.encode()
        guess_hash = hashlib.sha256(guess).hexdigest()
        return guess_hash[:4] == "0000"


# Instantiate the Node
app = Flask(__name__)

# Generate a globally unique address for this node
node_identifier = str(uuid4()).replace('-', '')

# Instantiate the Blockchain
blockchain = Blockchain()



@app.route('/mine', methods=['GET'])
def mine():
    # We run the proof of work algorithm to get the next proof...
    last_block = blockchain.last_block
    proof = blockchain.proof_of_work(last_block)

    # We must receive a reward for finding the proof.
    # The sender is "0" to signify that this node has mined a new coin.
    blockchain.new_transaction(
        sender="0",
        recipient=node_identifier,
        amount=blockchain.blockreward,
    )

    # Forge the new Block by adding it to the chain
    previous_hash = blockchain.hash(last_block)
    block = blockchain.new_block(proof, previous_hash)

    response = {
        'message': "New Block Forged",
        'index': block['index'],
        'transactions': block['transactions'],
        'proof': block['proof'],
        'previous_hash': block['previous_hash'],
    }
    return jsonify(response), 200

@app.route('/getwork', methods=['GET'])
def getwork():
    r =blockchain.getworkt()
    response = {
        'lastindex': r[0],
        'lasthash': r[1],
        'lastproof':r[2]
    }
    return jsonify(response), 200



@app.route('/transactions/new', methods=['POST'])
def new_transaction():
    values = request.get_json()

    # Check that the required fields are in the POST'ed data
    required = ['sender', 'recipient', 'amount']
    if not all(k in values for k in required):
        return 'Missing values', 400

    # Create a new Transaction
    index = blockchain.new_transaction(values['sender'], values['recipient'], values['amount'])

    response = {'message': f'Transaction will be added to Block {index}'}
    return jsonify(response), 201






@app.route('/chain', methods=['GET'])
def full_chain():
    response = {
        'chain': blockchain.chain,
        'length': len(blockchain.chain),
    }
    return jsonify(response), 200


@app.route('/nodes/register', methods=['POST'])
def register_nodes():
    values = request.get_json()

    nodes = values.get('nodes')
    if nodes is None:
        return "Error: Please supply a valid list of nodes", 400

    for node in nodes:
        blockchain.register_node(node)

    response = {
        'message': 'New nodes have been added',
        'total_nodes': list(blockchain.nodes),
    }
    return jsonify(response), 201

@app.route('/nodes', methods=['GET'])
def getnode():
    if len(request.args)>0:
        x=request.args.get("node")
        y= urllib.request.urlopen(x).read()
        my_json = y.decode('utf8').replace("'", '"')
        data = json.loads(my_json)

        return jsonify(data)
    else:
        return jsonify(blockchain.chain);

@app.route('/nodes/resolve', methods=['GET'])
def consensus():
    replaced = blockchain.resolve_conflicts

    if replaced:
        response = {
            'message': 'Our chain was replaced',
            'new_chain': blockchain.chain
        }
    else:
        response = {
            'message': 'Our chain is authoritative',
            'chain': blockchain.chain
        }

    return jsonify(response), 200

@app.route('/getwork/difficulty', methods=['GET'])
def getDiff():
    response = {
        'difficulty': blockchain.difficulty,
        'blockTime':blockchain.blocktime
    }
    return jsonify(response), 200

@app.route('/setdiff',methods=['POST'])
def setDiff():
    values = request.get_json()
    required = ['diff']
    if not all(k in values for k in required):
        return 'Missing values', 400
    blockchain.difficulty=values['diff']
    response={
        'message':"difficulty set to " +str(values['diff'])
    }
    return jsonify(response),200


@app.route('/submitwork', methods=['POST'])
def submit():
    values = request.get_json()
    last_block = blockchain.last_block
    # Check that the required fields are in the POST'ed data
    required = ['index','proof', 'adress']
    if not all(k in values for k in required):
        return 'Missing values', 400

    response = {}
    # Forge the new Block by adding it to the chain
    if values['index']==len(blockchain.chain)+1:
        previous_hash = blockchain.hash(last_block)
        block = blockchain.new_block(values['proof'], previous_hash)
        blockchain.new_transaction(
            sender="0",
            recipient=values['adress'],
            amount=blockchain.blockreward,
        )
        response = {
            'message': "New Block Forged",
            'index': block['index'],
            'transactions': block['transactions'],
            'proof': block['proof'],
            'previous_hash': block['previous_hash'],
        }

    return jsonify(response), 200



if __name__ == '__main__':
    from argparse import ArgumentParser

    parser = ArgumentParser()
    parser.add_argument('-p', '--port', default=5000, type=int, help='port to listen on')
    args = parser.parse_args()
    port = args.port

    app.run(host='localhost', port=port)
