const express = require('express')
const bodyParser = require('body-parser')
const uuidv4 = require('uuid/v4')
const crypto = require('crypto')
const request = require('request-promise');
const utf8 = require('utf8');

const app = express()
const port = process.env.PORT ? process.env.PORT : 5000

class Blockchain {
    constructor() {
        this.chain = []
        this.nodes = []
        this.pendingTransactions = []
        this.newBlock()
    }

    /**
     * Creates a new block containing any outstanding transactions
     *
     * @param proof
     * @param previousHash: the hash of the previous block
     */
    newBlock(proof, previousHash) {

        const block = {
            index: this.chain.length + 1,
            timestamp: new Date().toISOString(),
            transactions: this.pendingTransactions,
            proof: proof,
            previous_hash: previousHash,
        }
        this.chain.push(block)
        block.hash = Blockchain.hash(block)

        // Reset pending transactions
        this.pendingTransactions = []

        return block
    }

    /**
     * Generates a SHA-256 hash of the block
     * @param block
     */
    static hash(block) {
        const blockString = JSON.stringify(block, Object.keys(block).sort())
        return crypto.createHash("sha256").update(blockString).digest("hex")
    }

    /**
     * Gets the last block in the chain
     */
    lastBlock() {
        return this.chain.length && this.chain[this.chain.length - 1]
    }

    /**
     * Determines if a hash begins with a "difficulty" number of 0s
     *
     * @param hashOfBlock: the hash of the block (hex string)
     * @param difficulty: an integer defining the difficulty
     */
    static powIsAcceptable(hashOfBlock, difficulty) {
        return hashOfBlock.slice(0, difficulty) === "0".repeat(difficulty)
    }

    /**
     * Generates a random 32 byte string
     */
    static nonce() {
        return crypto.createHash("sha256").update(crypto.randomBytes(32)).digest("hex")
    }

    /**
     * Proof of Work mining algorithm
     *
     * We hash the block with random string until the hash begins with
     * a "difficulty" number of 0s.
     *
     * @param _block: the block to be mined (defaults to the last block)
     * @param difficulty: the mining difficulty to use
     */
    proofOfWork(_block = null, difficulty) {
        const block = _block || this.lastBlock()
        while (true) {
            block.nonce = Blockchain.nonce()
            if (Blockchain.powIsAcceptable(Blockchain.hash(block), difficulty)) {
                console.log("We mined a block!")
                console.log(` - Block hash: ${Blockchain.hash(block)}`)
                console.log(` - nonce:      ${block.nonce}`)
                return block
            }
        }
    }

    newTransaction(sender, recipient, amount) {
        this.pendingTransactions.push({
            sender,
            recipient,
            amount,
        })
        console.log(this.lastBlock())
        return this.lastBlock()['index'] + 1
    }

    /**
     * Add a new node to the list of nodes
     * @param address: Address of node. Eg. 'http://192.168.0.5:5000'
     */
    registerNode(address) {
        // todo: validate the url
        this.nodes.push(address)
    }

    /**
     * Determine if a given blockchain is valid
     * @param chain: A blockchain
     * @return boolean if valid, false if not
     */
    static validChain(chain) {
        let lastBlock = chain[0]
        let currentIndex = 1

        while (currentIndex < chain['length']) {
            const block = chain[currentIndex]

            // Check that the hash of the block is correct
            const lastBlockHash = this.hash(lastBlock)

            if (block['previous_hash'] !== lastBlockHash)
                return false

            // Check that the Proof of Work is correct
            if (!Blockchain.validProof(lastBlock['proof'], block['proof'], lastBlockHash))
                return false

            lastBlock = block
            currentIndex += 1
        }
        return true
    }


    /**
     * This is our consensus algorithm, it resolves conflicts
     * by replacing our chain with the longest one in the network.
     *
     * @return True if our chain was replaced, False if not
     */
    async resolveConflicts() {
        const neighbours = this.nodes
        let newChain = null

        // We're only looking for chains longer than ours
        let maxLength = this.chain['length']

        // Grab and verify the chains from all the nodes in our network
        for (const key in neighbours) {
            const node = neighbours[key]
            try {
                const response = await request({
                    uri: `http://${node}/chain`,
                    json: true
                })
                const length = response['length']
                const chain = response['chain']

                // Check if the length is longer and the chain is valid
                if (length > maxLength && Blockchain.validChain(chain)) {
                    maxLength = length
                    newChain = chain
                }
            } catch (e) {
                console.log("Something fishy happened!")
            }
        }

        // Replace our chain if we discovered a new, valid chain longer than ours
        if (newChain) {
            this.chain = newChain
            return true
        }
        return false
    }

    /**
     *  Validates the Proof
     *  @param lastProof
     *  @param proof: <int> Current Proof
     *  @param lastHash
     *  @return: <bool> True if correct, False if not.
     */
    static validProof(lastProof, proof, lastHash) {
        const guess = utf8.encode(lastProof + proof + lastHash)
        console.log({lastProof, proof, lastHash, sum: (lastProof + proof + lastHash), guess})

        const guessHash = crypto.createHash("sha256").update(guess).digest("hex")
        return guessHash.substr(0, 3) === "000"
    }


}

const difficulty = 3
const blockchain = new Blockchain()
blockchain.proofOfWork(null, difficulty)

const nodeIdentifier = uuidv4()


app.get('/mine', (req, res) => {
    // We run the proof of work algorithm to get the next proof...
    const lastBlock = blockchain.lastBlock()
    const proof = blockchain.proofOfWork(lastBlock, difficulty)

    // We must receive a reward for finding the proof.
    // The sender is "0" to signify that this node has mined a new coin.
    blockchain.newTransaction(
        "0",
        nodeIdentifier,
        1
    )

    // Forge the new Block by adding it to the chain
    let prevHash = Blockchain.hash(lastBlock)
    let block = blockchain.newBlock(proof, prevHash)

    return res.send({
        message: "New Block Forged",
        index: block['index'],
        transactions: block['transactions'],
        proof: block['proof'],
        previous_hash: block['previous_hash'],
    })
})

app.post('/transactions/new', bodyParser.json(), (req, res) => {
    // validate
    const {sender, recipient, amount} = req.body

    // create new transaction
    const index = blockchain.newTransaction(sender, recipient, amount)
    return res.status(201).send({'message': 'Transaction will be added to Block ' + index})
})

app.get('/chain', (req, res) => res.send({
    chain: blockchain.chain,
    length: blockchain.chain.length
}))

app.get('/nodes', (req, res) => res.send({nodes: blockchain.nodes}))

app.post('/nodes/register', bodyParser.json(), (req, res) => {
    const {nodes} = req.body
    if (!nodes.length)
        return res.status(400).send("Error: Please supply a valid list of nodes")
    nodes.forEach(node => blockchain.registerNode(node))
    return res.status(201).send({
        'message': 'New nodes have been added',
        'total_nodes': blockchain.nodes,
    })
})

app.get('/nodes/resolve', async (req, res) => {
    if (await blockchain.resolveConflicts())
        return res.send({
            'message': 'Our chain was replaced',
            'new_chain': blockchain.chain
        })
    else
        return res.send({
            'message': 'Our chain is authoritative',
            'chain': blockchain.chain
        })
})

app.listen(port, () => console.log(`Example app listening on port ${port}!`))