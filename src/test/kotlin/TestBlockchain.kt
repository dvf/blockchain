import org.junit.Test
import org.junit.Assert.assertThat
import org.hamcrest.CoreMatchers.`is`
import org.junit.Assert.assertEquals

class TestBlockchain {

    // TODO 不要かな？
    @Test fun hash値の一貫性を確認する() {
        var blockchain = Blockchain

        // blockの一つ目を用意する
        var transaction01: Transaction = Transaction("1", "2", 1)
        var transaction02: Transaction = Transaction("2", "3", 5)

        var transactionsA: MutableList<Transaction>? = null
        transactionsA?.add(transaction01)
        transactionsA?.add(transaction02)

        var blockA = Block(1, 1.1, transactionsA, 1, "hash")
        var hashA: String = blockchain.hash(blockA)

        // 同じtransactionだが別のインスタンスでblockを用意する
        var transactionsB: MutableList<Transaction>? = null
        transactionsB?.add(transaction01)
        transactionsB?.add(transaction02)

        var blockB = Block(1, 1.1, transactionsB, 1, "hash")
        var hashB: String = blockchain.hash(blockB)

        // 同じtransactionが入ったblockは同じhash値になることを確認する
        assertEquals(hashA, hashB)
    }
}