import blockchain.Blockchain
import blockchain.model.*
import org.junit.Test
import org.junit.Assert.*

class TestBlockchain {

    @Test fun hash値の一貫性を確認する() {
        val blockchain = Blockchain()

        // blockの一つ目を用意する
        val transaction01: Transaction = Transaction("1", "2", 1)
        val transaction02: Transaction = Transaction("2", "3", 5)

        val transactionsA: MutableList<Transaction> = mutableListOf()
        transactionsA.add(transaction01)
        transactionsA.add(transaction02)

        val blockA = Block(1, 1, transactionsA, 1, "hash")
        val hashA: String = blockchain.hash(blockA)

        // 同じtransactionだが別のインスタンスでblockを用意する
        val transactionsB: MutableList<Transaction> = mutableListOf()
        transactionsB.add(transaction01)
        transactionsB.add(transaction02)

        val blockB = Block(1, 1, transactionsB, 1, "hash")
        val hashB: String = blockchain.hash(blockB)

        // 同じtransactionが入ったblockは同じhash値になることを確認する
        assertEquals(hashA, hashB)
    }

    @Test fun proofOfWorkで発見したhash値の先頭4文字が0になっている() {
        val blockchain = Blockchain()
        val lastProof = "10"

        val proof = blockchain.proofOfWork(lastProof)
        println("proof：" + proof)

        val hashVal = blockchain.convertToHash(lastProof + proof)
        println("hash：" + hashVal)

        assertEquals("0000", hashVal.substring(0, 4))
    }

    @Test fun validChainメソッドでGenesisブロックがスキップされていること() {
        val blockchain = Blockchain()

        // Genesisブロックのみが入った状態のチェーンを検証
        val actual = blockchain.validChain(blockchain.chain)
        val expected = true
        assertEquals(expected, actual)
    }

    @Test fun validChainメソッドでハッシュが一致するときに判定処理がtrueになる() {
        val blockchain = Blockchain()

        // テスト用のチェーンの用意
        val transaction01: Transaction = Transaction("1", "2", 1)
        blockchain.newTransaction(transaction01)
        val transaction02: Transaction = Transaction("2", "3", 5)
        blockchain.newTransaction(transaction02)

        // マイニングの実施
        val lastBlock: Block = blockchain.lastBlock()
        val lastProof = lastBlock.proof
        val proof = blockchain.proofOfWork(lastProof.toString())
        blockchain.addBlock(proof)

        // 2ブロック(Genesisブロック含む)入ったチェーンを検証
        val actual = blockchain.validChain(blockchain.chain)
        val expected = true
        assertEquals(expected, actual)
    }

    @Test fun validChainメソッドでハッシュが不一致のときに判定処理がfalseになる() {
        val blockchain = Blockchain()

        // テスト用のチェーンの用意
        val transaction01: Transaction = Transaction("1", "2", 1)
        blockchain.newTransaction(transaction01)
        val transaction02: Transaction = Transaction("2", "3", 5)
        blockchain.newTransaction(transaction02)

        // マイニングの実施
        val lastBlock: Block = blockchain.lastBlock()
        val lastProof = lastBlock.proof
        val proof = blockchain.proofOfWork(lastProof.toString())
        blockchain.addBlock(proof)

        // ダミーチェーンの用意
        var dummyChain: MutableList<Block> = mutableListOf()
        val correctBlock = blockchain.lastBlock()
        val dummyBlock = Block(correctBlock.index, correctBlock.timestamp, correctBlock.transactions, correctBlock.proof, "AAAA")
        dummyChain.add(blockchain.chain.get(0))
        dummyChain.add(dummyBlock)

        // 2ブロック(Genesisブロック含む)入ったチェーンを検証
        val actual = blockchain.validChain(dummyChain)
        val expected = false
        assertEquals(expected, actual)
    }

}