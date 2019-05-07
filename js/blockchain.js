const crypto = require("crypto");


class Blockchain {
    constructor() {
        this.chain = [];
        this.pendingTransactions = [];
        this.newBlock();
    }

    /**
     * Creates a new block containing any outstanding transactions
     *
     * @param previousHash: the hash of the previous block
     * @param nonce: the random string used to make this block hash satisfy the difficulty requirements
     */
    newBlock(previousHash, nonce = null) {
        let block = {
            index: this.chain.length,
            timestamp: new Date().toISOString(),
            transactions: this.pendingTransactions,
            previousHash,
            nonce
        };

        block.hash = this.hash(block);

        console.log(`Created block ${block.index}`);

        // Add the new block to the blockchain
        this.chain.push(block);

        // Reset pending transactions
        this.pendingTransactions = [];
    }

    /**
     * Generates a SHA-256 hash of the block
     * @param block
     */
    hash(block) {
        const blockString = JSON.stringify(block, Object.keys(block).sort());
        return crypto.createHash("sha256").update(blockString).digest("hex");
    }

    /**
     * Gets the last block in the chain
     */
    lastBlock() {
        return this.chain.length && this.chain[this.chain.length - 1];
    }

    /**
     * Determines if a hash begins with a "difficulty" number of 0s
     *
     * @param hashOfBlock: the hash of the block (hex string)
     * @param difficulty: an integer defining the difficulty
     */
    powIsAcceptable(hashOfBlock, difficulty) {
        return hashOfBlock.slice(0, difficulty) == "0".repeat(difficulty);
    }

    /**
     * Generates a random 32 byte string
     */
    nonce() {
        return crypto.createHash("sha256").update(crypto.randomBytes(32)).digest("hex");
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
    proofOfWork(_block = null, difficulty = 4) {
        const block = _block || this.lastBlock();

        while (true) {
            block.nonce = this.nonce();
            if (this.powIsAcceptable(this.hash(block), difficulty)) {
                console.log("We mined a block!")
                console.log(` - Block hash: ${this.hash(block)}`);
                console.log(` - nonce:      ${block.nonce}`);
                return block;
            }
        }
    }
}

const blockchain = new Blockchain();
blockchain.proofOfWork(null, 5);
