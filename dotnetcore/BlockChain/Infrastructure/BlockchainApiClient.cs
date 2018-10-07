using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using BlockChain.Domain;
using BlockChain.Domain.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BlockChain.Infrastructure
{
    public class BlockchainApiClient : IBlockchainApiClient
    {
        public ICollection<IList<Block>> FindChains(IEnumerable<Node> nodes)
        {
            var chains = new List<IList<Block>>();
            foreach (var node in nodes)
            {
                try
                {
                    chains.Add(FindChain(node));
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex);
                }
            }
            return chains;
        }

        private IList<Block> FindChain(Node node)
        {
            var client = new RestClient(node.Address);
            var request = new RestRequest("blockchain/chain", Method.GET);
            request.AddHeader("accept", "application/json");

            var response = client.Execute<DTO.Blockchain>(request);
            if (response.StatusCode != HttpStatusCode.OK)
                throw new Exception($"Failed to grab blockchain from node: {node.Address.ToString()}");

            var blockchain = response.Data;
            if (blockchain.Chain.Count != blockchain.Length)
                throw new Exception("Received blockchain has an invalid length");

            return blockchain.Chain
                .Select(b => Block.Reconstitute(
                    b.Index, 
                    b.Timestamp, 
                    b.Proof,
                    b.PreviousHash, 
                    b.Transactions.Select(t => new Transaction(
                        t.Sender, 
                        t.Recipient, 
                        t.Amount))))
                .ToList();
        }
    }
}