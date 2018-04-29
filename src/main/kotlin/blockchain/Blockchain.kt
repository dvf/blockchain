package blockchain

import blockchain.model.*
import java.security.MessageDigest
import com.github.kittinunf.fuel.core.FuelManager
import com.github.kittinunf.fuel.httpGet

class Blockchain {
    var chain: MutableList<Block> = mutableListOf()
    var currentTransactions: MutableList<Transaction> = mutableListOf()
    var nodes: MutableMap<String, Node> = mutableMapOf()

    init {
        val genesisBlock: Block = Block(
                1,
                System.currentTimeMillis() / 1000,
                currentTransactions,
                100,
                "1"
        )
        // ジェネシスブロック追加
        chain.add(genesisBlock)
    }

    fun lastBlock(): Block {
        return chain.last()
    }

    fun addBlock(proof: String){
        // 登録されていたtransactionをリセット
        currentTransactions = mutableListOf()
        // 新しいブロックを作成
        val block = Block(
                chain.last().index + 1,
                System.currentTimeMillis() / 1000,
                currentTransactions,
                proof.toInt(),
                hash(chain.last())
        )
        chain.add(block)
    }

    fun registerNode(node: Node) {
        if (!nodes.containsKey(node.url)) {
            nodes.put(node.url, node)
        }
    }

    fun newTransaction(transaction: Transaction) {
        // 新しいトランザクションをリストに加える
        currentTransactions.add(transaction)
    }

    fun hash(block: Block): String {
        return convertToHash(block.toString())
    }

    // hash(前のproof + proof)の最初の4文字が0となるhash値を探す
    fun proofOfWork(lastProof: String): String {
        var proof = 0
        while (!validWork(lastProof, Integer.toString(proof))) {
            proof += 1
        }
        return Integer.toString(proof)
    }

    private fun validWork(lastProof: String, proof: String): Boolean {
        val hashVal = convertToHash(lastProof + proof)
        return hashVal.substring(0, 4) == "0000"
    }

    fun convertToHash(string: String): String {
        val digest = MessageDigest.getInstance("SHA-256").digest(string.toByteArray())
        val hashVal = digest.fold("", {str, it -> str + "%02x".format(it)})

        return hashVal
    }

    fun validChain(chain: MutableList<Block>): Boolean {

        chain.forEach {
            if (it.index == 1) {
                println("Genesisブロックのためスキップ")
                return@forEach
            }

            // previousHashと一つ前のブロックから再現したハッシュを比較していく
            val lastIndex = it.index - 1
            if (it.previousHash != hash(chain.get( lastIndex - 1))) {
                println("ハッシュが一致しない")
                return false
            }
        }

        return true
    }

    fun resolveConflicts(): Boolean {
        var maxLength = chain.count()
        nodes.forEach { nodeUrl, node ->
            FuelManager.instance.basePath = nodeUrl
            "/chain".httpGet().responseObject(GetChainRequest.Deserializer()) { request, response, result ->
                val (body, error) = result
                if (error == null) {
                    // TODO nodeUrl/chainより取得したchainが長い場合、validChain()を実行し判定をしてからそちらのチェーンを採用する
                    println("HTTPリクエスト成功")
                    println(body?.chain?.get(0)?.index)
                } else {
                    //error handling
                    println(error)
                }
            }
        }

        return true
    }
}