data class Block(
        val index: Int,
        val timestamp: Double,
        val transactions: MutableList<Transaction>?,
        val proof: Int,
        val previousHash: String
)