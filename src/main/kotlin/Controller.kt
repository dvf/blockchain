import blockchain.*
import blockchain.model.*
import com.fasterxml.jackson.databind.ObjectMapper
import spark.Route
import spark.Spark.halt

class Controller(private val objectMapper: ObjectMapper,
                 private val blockchain: Blockchain) {

    fun fullChain(): Route = Route { req, res ->
        blockchain.chain
    }

    fun mine(): Route = Route { req, res ->
        val lastBlock: Block = blockchain.lastBlock()
        val lastProof = lastBlock.proof
        val proof = blockchain.proofOfWork(lastProof.toString())// TODO proofの型を整理したい

        // proofを発見した報酬を獲得(senderを0とすることでマイニング実行者の報酬としている)
        blockchain.newTransaction(
                // TODO nodeIdentifierを実装(nodeに一意な識別子)
                Transaction("0", "node_id", 1)
        )
        // チェーンに新しいブロックを追加することで新しいブロック採掘完了
        blockchain.addBlock(proof)
        "新しいブロックを採掘しました"
    }

    fun registerNode(): Route = Route { req, res ->
        "新しいnodeを登録する"
    }

    fun resolveNode(): Route = Route { req, res ->
        "チェーンが確認されました"
    }

    fun addTransaction(): Route = Route { req, res ->
        val request: NewTransactionRequest =
                try {
                    objectMapper.readValue(req.bodyAsBytes(), NewTransactionRequest::class.java)
                } catch (e: Exception) {
                    throw halt(400)
                }
        val transaction = Transaction(request.sender,request.recipient,request.amount)
        blockchain.newTransaction(transaction)
        res.status(201)
        "トランザクションはブロックに追加されました"
    }
}