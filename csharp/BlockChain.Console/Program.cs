namespace BlockChainDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var chain = new BlockChain();
            var server = new WebServer(chain);
            System.Console.Read();
        }
    }
}
