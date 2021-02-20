package blockchain

import blockchain.model.*
import java.security.MessageDigest
import com.github.kittinunf.fuel.core.FuelManager
import com.github.kittinunf.fuel.httpGet
import com.github.kittinunf.result.Result

class Blockchain {
    var chain: MutableList<Block> = mutableListOf()
    var currentTransactions: MutableList<Transaction> = mutableListOf()
    var nodes: MutableMap<String, Node> = mutableMapOf()

    init {
        val genesisBlock: Block = Block(
                1,
                System.currentTimeMillis() / 1000,
                mutableListOf(),
                100,
                "1"
        )
        // Add the genesis block
        chain.add(genesisBlock)
    }

    fun lastBlock(): Block {
        return chain.last()
    }

    fun addBlock(proof: String){
        // Create a new block
        val block = Block(
                chain.last().index + 1,
                System.currentTimeMillis() / 1000,
                currentTransactions,
                proof.toInt(),
                hash(chain.last())
        )
        chain.add(block)

        // Reset transactions
        currentTransactions = mutableListOf()
    }

    fun registerNode(node: Node) {
        if (!nodes.containsKey(node.url)) {
            nodes.put(node.url, node)
        }
    }

    fun newTransaction(transaction: Transaction) {
        // add a new transaction
        currentTransactions.add(transaction)
    }

    fun hash(block: Block): String {
        return convertToHash(block.toString())
    }

    // Simple Proof of Work Algorithm:
    // - Find a number p' such that hash(pp') contains leading 4 zeroes
    // - Where p is the previous proof, and p' is the new proof
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

    // Determine if a given blockchain is valid
    fun validChain(chain: MutableList<Block>): Boolean {

        chain.forEach {
            if (it.index == 1) {
                return@forEach
            }

            // Check that the Proof of Work is correct
            val lastIndex = it.index - 1
            if (it.previousHash != hash(chain.get( lastIndex - 1))) {
                return false
            }
        }

        return true
    }

    fun resolveConflicts(): Boolean {
        var maxLength = chain.count()
        var currentChain = chain

        nodes.forEach { nodeUrl, node ->

            FuelManager.instance.basePath = nodeUrl
            val (request, response, result) = "/chain".httpGet().responseObject(GetChainRequest.Deserializer())

            when (result) {
                is Result.Success -> {
                    println("http request ok!")

                    val chain = result.value.chain
                    val length = result.value.length

                    // Check if the length is longer and the chain is valid
                    if(maxLength < length) {
                        val mutableChain = chain!!.toMutableList()
                        if (validChain(mutableChain)) {
                            maxLength = length
                            currentChain = mutableChain
                        }
                    }
                }

                is Result.Failure -> {
                    println("ERROR：" + result.error)
                }
            }
        }

        // Replace our chain if we discovered a new, valid chain longer than ours
        if (maxLength > chain.count()) {
            chain = currentChain

            return true
        }

        return false
    }
}