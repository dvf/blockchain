import spark.Spark.get

fun main(args: Array<String>){
    get("/hello"){req, res ->
        "Hello, world!"
    }
}