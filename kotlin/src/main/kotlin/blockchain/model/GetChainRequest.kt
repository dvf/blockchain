package blockchain.model

import com.github.kittinunf.fuel.core.ResponseDeserializable
import com.google.gson.Gson

data class GetChainRequest(val chain: ArrayList<Block>, val length: Int) {
    class Deserializer : ResponseDeserializable<GetChainRequest> {
        override fun deserialize(content: String) = Gson().fromJson(content, GetChainRequest::class.java)
    }
}