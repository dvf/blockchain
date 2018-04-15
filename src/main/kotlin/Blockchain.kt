import com.sun.jmx.snmp.Timestamp
import java.security.MessageDigest

object Blockchain {
    val chain: MutableList<Block>? = null
    val currentTransactions: MutableList<Transaction>? = null

    init {
        val genesisBlock: Block = Block(
                1,
                System.currentTimeMillis()/1000,
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
        // ブロックのSHA-256ハッシュを作る
        val digest = MessageDigest.getInstance("SHA-256").digest(block.toString().toByteArray())
        return digest.fold("", { str, it -> str + "%02x".format(it) })// TODO 各メソッドの仕様を再確認したい
    }

    // hash(前のproof + proof)の最初の4文字が0となるhash値を探す
    fun proofOfWork(lastProof: String): String {
        var proof = 0
        while (!validWork(lastProof, Integer.toString(proof))) {
            proof += 1
        }
        return Integer.toString(proof)
    }

    fun validWork(lastProof: String, proof: String): Boolean {
        val digest = MessageDigest.getInstance("SHA-256").digest((lastProof + proof).toByteArray())
        val hashVal = digest.fold("", {str, it -> str + "%02x".format(it)})

        return hashVal.substring(0, 4) == "0000"
    }

    fun convertHash(string: String): String {
        val digest = MessageDigest.getInstance("SHA-256").digest(string.toByteArray())
        val hashVal = digest.fold("", {str, it -> str + "%02x".format(it)})

        return hashVal
    }

}