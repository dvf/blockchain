import com.fasterxml.jackson.annotation.JsonProperty

class RegisterNodeRequest (
        @JsonProperty("url", required = true) val url: String
)