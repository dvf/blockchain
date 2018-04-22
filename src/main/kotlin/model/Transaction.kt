package blockchain.model

data class Transaction(
        val sender: String, // 送信者のアドレス
        val recipient: String, // 受信者のアドレス
        val amount: Int // 量
)