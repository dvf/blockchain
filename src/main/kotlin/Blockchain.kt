import com.sun.jmx.snmp.Timestamp

object Blockchain {
    val chain: MutableList<Block>? = null
    val currentTransactions: MutableList<Transaction>? = null

    init {
        // ジェネシスブロック定義
        val genesisBlock: Block = Block(
                1,
                123.0, // TODO 現在のtimestampを設定
                currentTransactions,
                100,
                "1"
        )
        // ジェネシスブロック追加
        newBlock(genesisBlock)
    }

    fun newBlock(block: Block){
        // 登録されていたtransactionをリセット
        currentTransactions?.dropWhile { true }
        // 新しいブロックを登録
        chain?.add(block)
    }

    fun newTransaction(transaction: Transaction) {
        // 新しいトランザクションをリストに加える
        currentTransactions?.add(transaction)
    }

    fun hash(block: Block): String {
        // TODO ブロックのSHA-256ハッシュを作る
        return "dummyHash"
    }

//    fun lastBlock() : Block {
//        // チェーンの最後のブロックをリターンする
//        return chain?.last() as Block
//    }
}