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
    val controller = Controller()

    path("/transactions") {
        get("/new", controller.addTransaction(), jsonTransformer)
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
        get("/register", controller.registerNode(), jsonTransformer)
        get("/resolve", controller.resolveNode(), jsonTransformer)
    }
}