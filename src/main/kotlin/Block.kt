data class Block(
        val index: Int,
        val timestamp: Double,
        val transactions: List<Transaction>,
        val proof: Int,
        val previousHash: String
)