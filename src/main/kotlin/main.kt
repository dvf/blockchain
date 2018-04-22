import blockchain.*
import spark.Spark.get
import spark.Spark.post
import spark.Spark.delete
import spark.Spark.path
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.module.kotlin.registerKotlinModule

fun main(args: Array<String>){

    val objectMapper = ObjectMapper().registerKotlinModule()
    val jsonTransformer = JsonTransformer(objectMapper)
    val blockChain = Blockchain()

    path("/transactions") {
        get("/new") { req, res ->
            "新しいBlockを採掘する"
        }
    }

    path("/mine") {
        // 新しいBlockを採掘する
        get("") { req, res ->
            "新しいBlockを採掘する"
        }
    }

    path("/chain") {
        // フルのブロックチェーンをリターンする
        get("") { req, res ->
            objectMapper.writeValueAsString(blockChain.chain)
        }
    }

    path("/nodes") {
        get("/register") { req, res ->
            "新しいnodeを登録する"
        }
        get("/resolve") { req, res ->
            "チェーンが確認されました"
        }
    }
}