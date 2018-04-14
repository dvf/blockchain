object Blockchain {
    val chain: List<Block>? = null
    val currentTransactions: List<Transaction>? = null

    fun newBlock(){
        // 新しいブロックを作り、チェーンに加える
    }

    fun newTransaction(){
        // 新しいトランザクションをリストに加える
    }

    fun hash(block: Block){
        // ブロックをハッシュ化する
    }

    fun lastBlock(){
        // チェーンの最後のブロックをリターンする
    }
}