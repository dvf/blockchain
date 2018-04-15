data class Block(
        val index: Int,
        val timestamp: Long,
        val transactions: MutableList<Transaction>?,
        val proof: Int,
        val previousHash: String
)