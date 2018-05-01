import blockchain.*
import blockchain.model.*
import com.fasterxml.jackson.databind.ObjectMapper
import spark.Route
import spark.Spark.halt

class Controller(private val objectMapper: ObjectMapper,
                 private val blockchain: Blockchain,
                 private val nodeId: String) {

    fun fullChain(): Route = Route { req, res ->
        Chain(blockchain.chain, blockchain.chain.count())
    }

    fun mine(): Route = Route { req, res ->
        val lastBlock: Block = blockchain.lastBlock()
        val lastProof = lastBlock.proof
        val proof = blockchain.proofOfWork(lastProof.toString())

        // We must receive a reward for finding the proof.
        // The sender is "0" to signify that this node has mined a new coin.
        blockchain.newTransaction(
                Transaction("0", nodeId, 1)
        )
        // Forge the new Block by adding it to the chain
        blockchain.addBlock(proof)
        "A new block have been added"
    }

    fun registerNode(): Route = Route { req, res ->
        val request: RegisterNodeRequest =
                try {
                    objectMapper.readValue(req.bodyAsBytes(), RegisterNodeRequest::class.java)
                } catch (e: Exception) {
                    throw halt(400)
                }
        val node = Node(request.url)
        blockchain.registerNode(node)
        "New nodes have been added"
    }

    fun resolveNode(): Route = Route { req, res ->
        val replaced = blockchain.resolveConflicts()
        val message: String
        if (replaced) {
            message = "Our chain was replaced"
        } else {
            message = "Our chain is authoritative"
        }
        res.status(200)
        message
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
        "A new transaction have been added"
    }
}