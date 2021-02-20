import blockchain.Blockchain
import blockchain.model.*
import org.junit.Test
import org.junit.Assert.*

class TestBlockchain {

    @Test fun TestProofOfWork() {
        val blockchain = Blockchain()
        val lastProof = "10"

        val proof = blockchain.proofOfWork(lastProof)
        println("proof：" + proof)

        val hashVal = blockchain.convertToHash(lastProof + proof)
        println("hash：" + hashVal)

        assertEquals("0000", hashVal.substring(0, 4))
    }

    @Test fun TestValidChainMethodInNominalCase() {
        val blockchain = Blockchain()

        val transaction01: Transaction = Transaction("1", "2", 1)
        blockchain.newTransaction(transaction01)
        val transaction02: Transaction = Transaction("2", "3", 5)
        blockchain.newTransaction(transaction02)

        val lastBlock: Block = blockchain.lastBlock()
        val lastProof = lastBlock.proof
        val proof = blockchain.proofOfWork(lastProof.toString())
        blockchain.addBlock(proof)

        val actual = blockchain.validChain(blockchain.chain)
        val expected = true
        assertEquals(expected, actual)
    }

    @Test fun TestValidChainMethodInNonNominalCase() {
        val blockchain = Blockchain()

        val transaction01: Transaction = Transaction("1", "2", 1)
        blockchain.newTransaction(transaction01)
        val transaction02: Transaction = Transaction("2", "3", 5)
        blockchain.newTransaction(transaction02)

        val lastBlock: Block = blockchain.lastBlock()
        val lastProof = lastBlock.proof
        val proof = blockchain.proofOfWork(lastProof.toString())
        blockchain.addBlock(proof)

        var dummyChain: MutableList<Block> = mutableListOf()
        val correctBlock = blockchain.lastBlock()
        val dummyBlock = Block(correctBlock.index, correctBlock.timestamp, correctBlock.transactions, correctBlock.proof, "AAAA")
        dummyChain.add(blockchain.chain.get(0))
        dummyChain.add(dummyBlock)

        val actual = blockchain.validChain(dummyChain)
        val expected = false
        assertEquals(expected, actual)
    }

}