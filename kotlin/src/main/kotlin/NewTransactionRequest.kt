import com.fasterxml.jackson.annotation.JsonProperty

data class NewTransactionRequest (
        @JsonProperty("sender", required = true) val sender: String,
        @JsonProperty("recipient", required = true) val recipient: String,
        @JsonProperty("amount", required = true) val amount: Int
)