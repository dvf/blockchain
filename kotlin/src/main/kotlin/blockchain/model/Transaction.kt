package blockchain.model

data class Transaction(
        val sender: String,
        val recipient: String,
        val amount: Int
)