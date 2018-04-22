import blockchain.*
import spark.Route

class Controller {

    fun fullChain(): Route = Route { req, res ->
        // TODO Blockchainインスタンスをコンストラクタに持ってく
        val blockChain = Blockchain()
        blockChain.chain
    }

    fun mine(): Route = Route { req, res ->
        "新しいBlockを採掘する"
    }

    fun registerNode(): Route = Route { req, res ->
        "新しいnodeを登録する"
    }

    fun resolveNode(): Route = Route { req, res ->
        "チェーンが確認されました"
    }
}