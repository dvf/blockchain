namespace BlockChainDemo.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            string host = null;
            string port = null;
            if (args.Length == 2)
            {
                host = args[0];
                port = args[1];
            }

            var chain = new BlockChain();
            var server = new WebServer(chain,host,port);
            server.Start();
            System.Console.Read();
        }
    }
}
