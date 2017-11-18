using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;

namespace BlockChainDemo
{
    public class WebServer
    {
        public WebServer(BlockChain chain)
        {
            var server = new TinyWebServer.WebServer(request =>
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

                        //GET: http://localhost:12345/nodes/register
                        case "/nodes/resolve":
                            return chain.Consensus();
                    }

                    return "";
                },
                "http://localhost:12345/mine/",
                "http://localhost:12345/transactions/new/",
                "http://localhost:12345/chain/",
                "http://localhost:12345/nodes/register/",
                "http://localhost:12345/nodes/resolve/"
            );

            server.Run();
        }
    }
}
