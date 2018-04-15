import spark.Spark.get
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.module.kotlin.registerKotlinModule

fun main(args: Array<String>){

    val objectMapper = ObjectMapper().registerKotlinModule()
    val blockChain: Blockchain = Blockchain()

    // 新しいBlockを採掘する
    get("/mine") { req, res ->
        "新しいBlockを採掘する"
    }

    // フルのブロックチェーンをリターンする
    get("/chain") { req, res ->
        objectMapper.writeValueAsString(blockChain.chain)
    }
}