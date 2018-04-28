import blockchain.*
import spark.Spark.get
import spark.Spark.post
import spark.Spark.delete
import spark.Spark.path
import com.fasterxml.jackson.databind.ObjectMapper
import com.fasterxml.jackson.module.kotlin.registerKotlinModule
import java.util.UUID

fun main(args: Array<String>){

    val nodeId = UUID.randomUUID().toString().replace("-", "")
    val objectMapper = ObjectMapper().registerKotlinModule()
    val jsonTransformer = JsonTransformer(objectMapper)
    val blockChain = Blockchain()
    val controller = Controller(objectMapper, blockChain, nodeId)


    path("/transactions") {
        post("/new", controller.addTransaction(), jsonTransformer)
    }

    path("/mine") {
        // 新しいBlockを採掘する
        get("", controller.mine(), jsonTransformer)
    }

    path("/chain") {
        // フルのブロックチェーンをリターンする
        get("", controller.fullChain(), jsonTransformer)
    }

    path("/nodes") {
        post("/register", controller.registerNode(), jsonTransformer)
        get("/resolve", controller.resolveNode(), jsonTransformer)
    }
}