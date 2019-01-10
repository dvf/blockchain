using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;

namespace BlockChainDemo
{
    public class BlockChain
    {
        private List<Transaction> _currentTransactions = new List<Transaction>();
        private List<Block> _chain = new List<Block>();
        private List<Node> _nodes = new List<Node>();
        private Block _lastBlock => _chain.Last();

        public string NodeId { get; private set; }

        //ctor
        public BlockChain()
        {
            NodeId = Guid.NewGuid().ToString().Replace("-", "");
            CreateNewBlock(proof: 0, previousHash: "0"); //genesis block
        }

        //private functionality
        private void RegisterNode(string address)
        {
            _nodes.Add(new Node { Address = new Uri(address) });
        }

        private bool IsValidChain(List<Block> chain)
        {
            Block block = null;
            Block lastBlock = chain.First();
            int currentIndex = 1;
            while (currentIndex < chain.Count)
            {
                block = chain.ElementAt(currentIndex);
                Debug.WriteLine($"{lastBlock}");
                Debug.WriteLine($"{block}");
                Debug.WriteLine("----------------------------");

                //Check that the hash of the block is correct
                if (block.PreviousHash != GetHash(lastBlock))
                    return false;

                //Check that the Proof of Work is correct
                if (!IsValidProof(lastBlock.Proof, block.Proof, lastBlock.PreviousHash))
                    return false;

                lastBlock = block;
                currentIndex++;
            }

            return true;
        }

        private ResolveStatus ResolveConflicts()
        {
            List<Block> newChain = null;
            ResolveStatus resolveStatus = new ResolveStatus();
            int maxLength = _chain.Count;

            foreach (Node node in _nodes)
            {
                var url = new Uri(node.Address, "/chain");
                var request = (HttpWebRequest)WebRequest.Create(url);
                HttpWebResponse response;
                try
                {
                    
                    response = (HttpWebResponse)request.GetResponse();
                }
                catch (Exception exception)
                {
                    resolveStatus.ResolveResults.Add(new NodeResolveResult()
                    {
                        NodeUrl = node.Address.ToString(),
                        Status = false,
                        ResultMessage = exception.Message
                    });
                    continue;
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var model = new
                    {
                        chain = new List<Block>(),
                        length = 0
                    };
                    string json = new StreamReader(response.GetResponseStream()).ReadToEnd();
                    var data = JsonConvert.DeserializeAnonymousType(json, model);

                    if (data.chain.Count > _chain.Count && IsValidChain(data.chain))
                    {
                        maxLength = data.chain.Count;
                        newChain = data.chain;
                       
                    }
                    resolveStatus.ResolveResults.Add(new NodeResolveResult()
                    {
                        NodeUrl = node.Address.ToString(),
                        Status = true,
                        ResultMessage = "Sync Success!"
                    });
                }
                else
                {
                    resolveStatus.ResolveResults.Add(new NodeResolveResult()
                    {
                        NodeUrl = node.Address.ToString(),
                        Status = false,
                        ResultMessage = response.StatusCode.ToString()
                    });
                    continue;
                }
            }

            if (newChain != null)
            {
                _chain = newChain;
                resolveStatus.Status = true;
                return resolveStatus;
            }
            resolveStatus.Status = false;
            return resolveStatus;
        }

        private Block CreateNewBlock(int proof, string previousHash = null)
        {
            var block = new Block
            {
                Index = _chain.Count,
                Timestamp = DateTime.UtcNow,
                Transactions = _currentTransactions.ToList(),
                Proof = proof,
                PreviousHash = previousHash ?? GetHash(_chain.Last())
            };
            block.Hash = GetSha256(block.ToString());
            _currentTransactions.Clear();
            _chain.Add(block);
            return block;
        }

        private int CreateProofOfWork(int lastProof, string previousHash)
        {
            int proof = 0;
            while (!IsValidProof(lastProof, proof, previousHash))
                proof++;

            return proof;
        }

        private bool IsValidProof(int lastProof, int proof, string previousHash)
        {
            string guess = $"{lastProof}{proof}{previousHash}";
            string result = GetSha256(guess);
            return result.StartsWith("0000");
        }

        private string GetHash(Block block)
        {
            string blockText = JsonConvert.SerializeObject(block);
            return GetSha256(blockText);
        }

        private string GetSha256(string data)
        {
            var sha256 = new SHA256Managed();
            var hashBuilder = new StringBuilder();

            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] hash = sha256.ComputeHash(bytes);

            foreach (byte x in hash)
                hashBuilder.Append($"{x:x2}");

            return hashBuilder.ToString();
        }

        //web server calls
        internal string Mine()
        {
            int proof = CreateProofOfWork(_lastBlock.Proof, _lastBlock.PreviousHash);

            //CreateTransaction(sender: "0", recipient: NodeId, amount: 1);
            Block block = CreateNewBlock(proof /*, _lastBlock.PreviousHash*/);

            var response = new
            {
                Message = "New Block Forged",
                Index = block.Index,
                Transactions = block.Transactions.ToArray(),
                Proof = block.Proof,
                PreviousHash = block.PreviousHash
            };

            return JsonConvert.SerializeObject(response);
        }

        internal string GetFullChain()
        {
            var response = new
            {
                chain = _chain.ToArray(),
                length = _chain.Count
            };

            return JsonConvert.SerializeObject(response);
        }

        internal string RegisterNodes(string[] nodes)
        {
            var builder = new StringBuilder();
            foreach (string node in nodes)
            {
                string url = $"http://{node}";
                RegisterNode(url);
                builder.Append($"{url}, ");
            }

            builder.Insert(0, $"{nodes.Count()} new nodes have been added: ");
            string result = builder.ToString();
            return result.Substring(0, result.Length - 2);
        }

        internal string GetAllRegisteredNodes()
        {
            var response = new
            {
                nodes = _nodes.ToArray(),
                length = _nodes.Count
            };
            return JsonConvert.SerializeObject(response);
        }

        internal string Consensus()
        {
            var resoveStatus = ResolveConflicts();
            bool replaced = resoveStatus.Status;
            string message = replaced ? "was replaced" : "is authoritive";
            
            var response = new
            {
                ResolveStatus = resoveStatus,
                Message = $"Our chain {message}",
                Chain = _chain
            };

            return JsonConvert.SerializeObject(response);
        }

        internal int CreateTransaction(string sender, string recipient, int amount)
        {
            var transaction = new Transaction
            {
                Sender = sender,
                Recipient = recipient,
                Amount = amount
            };

            _currentTransactions.Add(transaction);

            return _lastBlock != null ? _lastBlock.Index + 1 : 0;
        }

        public List<Block> GetBlocks()
        {
            return _chain;
        }
    }
}
