data class Block(
        val index: Int,
        val timestamp: Int,
        val transactions: List<Block>,
        val proof: Int,
        val previousHash: String
)