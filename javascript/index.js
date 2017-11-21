var express = require('express')
var parse = require('url-parse')
var app = express()
var bodyParser = require('body-parser');  
app.use(bodyParser.json()); // support json encoded bodies
app.use(bodyParser.urlencoded({ extended: true })); // support encoded bodies
var sha256 = require('sha256')
const requests = require('request')


// Blockchain class
class Blockchain {
  
  constructor() {
    this.current_transactions = []
    this.chain = new Array()
    this.nodes = new Set()
    // Create the genesis block
    this.newBlock({proof: 100, previous_hash: '1'})
    
  }
 
  lastBlock(){
    return this.chain[this.chain.length-1]
  }

  registerNode(address) {
    /*
    Add a new node to the list of nodes
    
    :param address: Address of node. Eg. 'http://192.168.0.5:5000'
    */

    parsed_url = parse(address)
    this.nodes.push(parsed_url.host)
  }

  validChain(chain) {
    /*
    Determin if a given blockchain is valid

    : param chain: A blockchain
    : return: True if valid, False if not
    */
    
    var lastBlock = chain[0]
    
    var currentIndex = 1

    while(currentIndex < chain.length) {
      var block = chain[currentIndex]
      console.log(`${lastBlock}`)
      console.log(`${block}`)
      console.log("\n------------\n")
      // Check that the hash of the block is correct
      if(block['previous_hash'] != this.hash(lastBlock)) {
        return false
      }
      
      // Check that the Proof of Work is correct
      if(!(this.constructor.validProof(lastBlock['proof'], block['proof']))){
        return false
      }

      lastBlock = block
      currentIndex += 1
    }
    
    return true
  }

  static hash(block){
    /*
    Creates a SHA-256 hash of a Block

    : param block: Block
    */

    // We must make sure that the Object is Ordered, or we'll have inconsistent hashes
    const blockString = JSON.stringify(block, Object.keys(block).sort())
    return sha256(blockString)
  }

  resolveConflicts() {
    /*
    This is our consensus algorithm, it resolves conflicts
    by replacing our chain with the longest one in the network.

    : return: True if our chain was replaced, False if not
    */

    neighbours = this.nodes
    newChain = null

    // We're only looking for chains longer than ours
    max_length = this.chain.length

    // Grab and verify the chains from all the nodes in our network
    neighbours.forEach(function(node) {
      response = requests.get(`http://${node}/chain`)
                         .on('response', function(res) {
                          if(res.statusCode == 200){
                            length = res.json()['length']
                            chain = res.json()['chain']

                            // Check if the length is longer and the chain is valid
                            if(length > max_length && this.validChain(chain)) {
                               max_length = length
                               newChain = chain
                            }
                          }
                 })
    });

    if(newChain){ this.chain = newChain; return true}


    return false;
  }

  newBlock(proof, previous_hash){
    /* 
    Create a new Block in the Blockchain

    : param proof: The proof given by the Proof of Work algorithm
    : param previous_hash: Hash of previous Block
    : return: New Block
    */

    const block = {
      'index': this.chain.length + 1,
      'timestamp': Date.now(),
      'transactions': this.current_transactions,
      'proof': proof,
      'previous_hash': previous_hash !== null ?  previous_hash : this.constructor.hash(this.chain.slice(-1)[0]), //Optional funtion argument
    }

    this.current_transactions = []
     
    this.chain.push(block)
    
    return block
  }

  newTransaction(sender, recipient, amount) {
    /* 
    Creates a new transaction to go into the next mined Block

    : param sender: Address of the Sender
    : param recipient: Address of the Recipient
    : param amount: Amount
    : return: The index of the Block that will hold this transaction
    */

    this.current_transactions.push({
      'sender': sender,
      'recipient': recipient,
      'amount': amount,
    })

    return this.lastBlock()['index'] + 1
  }

  proofOfWork(lastProof) {
    /*
    Simple Proof of Work Algorithm:
    - Find a number p' such that hash(pp') contains leading 4 zeroes, where p is the previous p'
    - p is the previous proof, and p' is the new proof
    */

    var proof = 0
    while(this.constructor.validProof(lastProof, proof) == false) {
       proof += 1
    }

    return proof
  }

  static validProof(lastProof, proof) {
    /*
    Validates the Proof

    : param lastProof: Previous Proof
    : param proof: Current Proof
    : return: True if correct, False if not.
    */
    
    const guess = sha256(`${lastProof}${proof}`)
    return guess.slice(0,4) === "0000"
  }
}





// Generate a globally unique address for this node
function uuidv4() {
  return 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
    var r = Math.random() * 16 | 0, v = c == 'x' ? r : (r & 0x3 | 0x8);
    return v.toString(16);
  });
}
node_identifier = uuidv4()

blockChain = new Blockchain()

app.get('/mine', function mine(req, res) {
  // We run the proof of work algorithm to get the next proof...
  lastBlock = blockChain.lastBlock()
  lastProof = blockChain.lastProof
  console.log(blockChain.chain)
  proof = blockChain.proofOfWork(lastProof)

  // We must receive a reward for finding the proof.
  // The sender is "0" to signify that this node has mined a new coin.
  blockChain.newTransaction(
    sender="0",
    recipient=node_identifier,
    amount=1,
  )

  // Forge the new Block by adding it to the chain
  previousHash = blockChain.constructor.hash(lastBlock)
  block = blockChain.newBlock(proof, previousHash)
  console.log(lastBlock)
  response = {
    'message': "New Block Forged",
    'index': block['index'],
    'transactions': block['transactions'],
    'proof': block['proof'],
    'previous_hash': block['previous_hash'],
  }

  res.status(200).json(response)
})

app.post('/transactions/new', function newTransaction(req, res) {
  const values = req.body 
  const keys = Object.keys(values)
  // Check that the required fields are in the POST'ed data
  const required = ['sender', 'recipient', 'amount']
  if(keys.toString() !== required.toString()) {
    res.status(400).send("Missing values")
  }

  
  // Create a new Transaction
  const index = blockChain.newTransaction(values['sender'], values['recipient'], values['amount'])

  response = {'message': `Transaction will be added to Block ${index}`}

  res.status(201).json(response)
})

app.get('/chain', function fullChain(req, res){
  response = {
    'chain': blockChain.chain,
    'length': blockChain.chain.length,
  }

  res.status(200).json(response)
})


app.post('/nodes/register', function registerNodes(req, res){ 
  values = req.body

  nodes = values['nodes']

  if(nodes == null){
    res.status(400).send("Error: Please supply a valid list of nodes")
  }

  nodes.forEach(function(node) {
    blockChain.registerNode(node)
  })

  response = {
    'message': "New nodes have been added",
    'total_nodes': Array(blockChain.nodes)
  }

  res.status(201).json(response)
})

app.get('/nodes/resolve', function consensus(req, res){
  replaced = blockChain.resolveConflicts()

  if(replaced) {
    response = {
      'message': "Our chain was replaced",
      'new_chain': blockChain.chain
    }
  }
  else {
    response = {
      'message': "Our chain is authoritative",
      'new_chain': blockChain.chain
    }
  }

  res.status(200).json(response)
})

app.listen(3000)
