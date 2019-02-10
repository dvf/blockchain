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
                            chain.Mine();
                            return $"Mining process is started";

                        //POST: http://localhost:12345/transactions/new
                        //{ "Amount":123, "Recipient":"ebeabf5cc1d54abdbca5a8fe9493b479", "Sender":"31de2e0ef1cb4937830fcfd5d2b3b24f" }
                        case "/transactions/new":
                            if (request.HttpMethod != HttpMethod.Post.Method)
                                return $"{new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)}";

                            json = new StreamReader(request.InputStream).ReadToEnd();
                            Transaction trx = JsonConvert.DeserializeObject<Transaction>(json);
                            int blockId = chain.CreateTransaction(trx.Id,trx.Sender, trx.Recipient, trx.Amount,trx.Signature);
                            if (blockId > 0)
                            {
                                return $"Your transaction will be included in block {blockId}";
                            }
                            else
                            {
                                return "Your transaction is invalid";
                            }
                        //POST: http://localhost:12345/transactions/add
                        //{ "Id": "123", "Amount":123, "Recipient":"ebeabf5cc1d54abdbca5a8fe9493b479", "Sender":"31de2e0ef1cb4937830fcfd5d2b3b24f" }
                        case "/transactions/add":
                            if (request.HttpMethod != HttpMethod.Post.Method)
                                return $"{new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)}";

                            json = new StreamReader(request.InputStream).ReadToEnd();
                            Transaction incomeTrx = JsonConvert.DeserializeObject<Transaction>(json);
                            chain.AddTransaction(incomeTrx.Id, incomeTrx.Sender, incomeTrx.Recipient, incomeTrx.Amount, incomeTrx.Signature);
                            return $"Your transaction has been added";
                        //POST: http://localhost:12345/transactions/edit
                        //{ "Id": "123", "Amount":123, "Recipient":"ebeabf5cc1d54abdbca5a8fe9493b479", "Sender":"31de2e0ef1cb4937830fcfd5d2b3b24f" }
                        case "/transactions/edit":
                            if (request.HttpMethod != HttpMethod.Post.Method)
                                return $"{new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)}";

                            json = new StreamReader(request.InputStream).ReadToEnd();
                            Transaction newTrx = JsonConvert.DeserializeObject<Transaction>(json);
                            chain.EditTransaction(newTrx.Id, newTrx.Sender, newTrx.Recipient, newTrx.Amount, newTrx.Signature);
                            return $"Your transaction has been edited";


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

                        //GET: http://localhost:12345/nodes/update
                        case "/consensus/request":
                             chain.RequestUpdate();
                            return "A consensus request has been sent";
                        //POST: http://localhost:12345/balance/query
                        //{ "Owner": "Node 1"}
                        case "/balance/query":
                            if (request.HttpMethod != HttpMethod.Post.Method)
                                return $"{new HttpResponseMessage(HttpStatusCode.MethodNotAllowed)}";

                            json = new StreamReader(request.InputStream).ReadToEnd();
                            var ownerQuery = new { Owner = "" };
                            var owner = JsonConvert.DeserializeAnonymousType(json, ownerQuery);
                            var balance = chain.QueryBalance(owner.Owner);
                            return balance.ToString();
                        //GET: http://localhost:12345/transactions/pool
                        case "/transactions/pool":
                            return chain.GetTransactionsMemTool();
                    }

                    return "";
                },
                $"http://{_host}:{_port}/mine/",
                $"http://{_host}:{_port}/transactions/new/",
                $"http://{_host}:{_port}/transactions/add/",
                $"http://{_host}:{_port}/transactions/edit/",
                $"http://{_host}:{_port}/chain/",
                $"http://{_host}:{_port}/nodes/register/",
                $"http://{_host}:{_port}/nodes/",
                $"http://{_host}:{_port}/nodes/resolve/",
                $"http://{_host}:{_port}/consensus/request/",
                $"http://{_host}:{_port}/balance/query/",
                $"http://{_host}:{_port}/transactions/pool/"

            );
            
            
        }

        public void Start()
        {
            _server.Run();
            Console.WriteLine($"Server is now listining on http://{_host}:{_port}");

        }
    }
}
