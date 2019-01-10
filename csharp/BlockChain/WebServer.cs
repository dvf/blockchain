using System;
using Newtonsoft.Json;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;

namespace BlockChainDemo
{
    public class WebServer
    {
        TinyWebServer.WebServer _server;
        readonly string _host;
        readonly string _port;

        public string Url { get; set; }

        public WebServer(BlockChain chain, string host = null,string port = null)
        {
            var settings = ConfigurationManager.AppSettings;
           
            _host = string.IsNullOrEmpty(host)
                ? (settings["host"]?.Length > 1 ? settings["host"] : "localhost")
                : host;

          
            _port = string.IsNullOrEmpty(port)
                ?(settings["port"]?.Length > 1 ? settings["port"] : "12345"):port;

            Url = $"http://{_host}:{_port}/";
            _server = new TinyWebServer.WebServer(request =>
                {
                    string path = request.Url.PathAndQuery.ToLower();
                    string query = "";
                    string json = "";
                    if (path.Contains("?"))
                    {
                        string[] parts = path.Split('?');
                        path = parts[0];
                        query = parts[1];
                    }

                    switch (path)
                    {
                        //GET: http://localhost:12345/mine
                        case "/mine":
                            return chain.Mine();

                        //POST: http://localhost:12345/transactions/new
                        //{ "Amount":123, "Recipient":"ebeabf5cc1d54abdbca5a8fe9493b479", "Sender":"31de2e0ef1cb4937830fcfd5d2b3b24f" }
                        case "/transactions/new":
                            if (request.HttpMethod != HttpMethod.Post.Method)
                                return $"{new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)}";

                            json = new StreamReader(request.InputStream).ReadToEnd();
                            Transaction trx = JsonConvert.DeserializeObject<Transaction>(json);
                            int blockId = chain.CreateTransaction(trx.Sender, trx.Recipient, trx.Amount);
                            return $"Your transaction will be included in block {blockId}";

                        //GET: http://localhost:12345/chain
                        case "/chain":
                            return chain.GetFullChain();

                        //POST: http://localhost:12345/nodes/register
                        //{ "Urls": ["localhost:54321", "localhost:54345", "localhost:12321"] }
                        case "/nodes/register":
                            if (request.HttpMethod != HttpMethod.Post.Method)
                                return $"{new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)}";

                            json = new StreamReader(request.InputStream).ReadToEnd();
                            var urlList = new { Urls = new string[0] };
                            var obj = JsonConvert.DeserializeAnonymousType(json, urlList);
                            return chain.RegisterNodes(obj.Urls);
                        
                        //GET http://localhost:12345/nodes
                        case "/nodes":
                            return chain.GetAllRegisteredNodes();
                        
                        //GET: http://localhost:12345/nodes/resolve
                        case "/nodes/resolve":
                            return chain.Consensus();
                    }

                    return "";
                },
                $"http://{_host}:{_port}/mine/",
                $"http://{_host}:{_port}/transactions/new/",
                $"http://{_host}:{_port}/chain/",
                $"http://{_host}:{_port}/nodes/register/",
                $"http://{_host}:{_port}/nodes/",
                $"http://{_host}:{_port}/nodes/resolve/"
            );
            
            
        }

        public void Start()
        {
            _server.Run();
            Console.WriteLine($"Server is now listining on http://{_host}:{_port}");

        }
    }
}
