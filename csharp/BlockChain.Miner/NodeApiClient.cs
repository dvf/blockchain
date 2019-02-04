using System;
using BlockChain.Miner.Properties;
using BlockChainDemo;
using Newtonsoft.Json;
using RestSharp;

namespace BlockChain.Miner
{
    internal class NodeApiClient
    {
        private static string _baseUrl;
        private static RestClient _client;
        static NodeApiClient()
        {
            _baseUrl = $"http://{WebServerSettings.Default.Host}:{WebServerSettings.Default.Port}/";
            _client = new RestClient(_baseUrl);

        }
        public static dynamic LoadBlockChain()
        {
            var response = _client.Get(new RestRequest("/chain"));
            var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return data;
        }

        public static string SendTransaction(Transaction transaction)
        {
            var req = new RestRequest("/transactions/new",Method.POST);
            req.AddJsonBody(transaction);
            var response = _client.Post<Transaction>(req);
            return response.Content;
        }

        public static string  MineBlock()
        {
            var response = _client.Get(new RestRequest("/mine"));
            //var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return response.Content;
        }

        public static string AddPeer(string url)
        {
            var req = new RestRequest("/nodes/register", Method.POST);
            var urlBody = new {Urls = new String[] {url}};
            req.AddJsonBody(urlBody);
            var response = _client.Post<Transaction>(req);
            return response.Content;
        }

        public static string RequestConsensus()
        {
            var response = _client.Get(new RestRequest("consensus/request"));
            var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return data.Message;
        }

        public static string ValidateConsensus()
        {
            //_client.Get(new RestRequest("nodes/resolve"));
            //return "a Valide Consensus request has been sent";

            var response = _client.Get(new RestRequest("nodes/resolve"));
            var data = JsonConvert.DeserializeObject<dynamic>(response.Content);
            return data.Message;
        }

        public static string QueryBalance()
        {
            var req = new RestRequest("/balance/query", Method.POST);
            var queryBody = new { Owner = WebServerSettings.Default.NodeName };
            req.AddJsonBody(queryBody);
            var response = _client.Post(req);
            return response.Content;
        }
    }
}
