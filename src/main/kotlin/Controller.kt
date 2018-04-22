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
        "新しいBlockを採掘する"
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