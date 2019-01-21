using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainDemo
{

    public class ShowNounceEventArgs : EventArgs
    {
        public ShowNounceEventArgs(string s)
        {
            Message = s;
        }

        public string Message { get; }
    }

    public class BlockChain
    {
        private List<Transaction> _currentTransactions = new List<Transaction>();
        private List<Block> _chain = new List<Block>();
        private List<Node> _nodes = new List<Node>();
        private Block _lastBlock => _chain.Last();
        private string difficultyLevel = "00000";
        private static bool cancelMining = false;
        public event EventHandler NewBlockMined;
        public event EventHandler BlockMiningCanceled;
        public event EventHandler ConsensusValidateRequest;
        public delegate void ShowCurrentNounceEventHandler(object sender, ShowNounceEventArgs eventArgs);
        public event ShowCurrentNounceEventHandler ShowCurrentNounce;

        public string NodeId { get; private set; }

        //ctor
        public BlockChain()
        {
            NodeId = Guid.NewGuid().ToString().Replace("-", "");
            //var gensisProof = ProofOfWork(new List<Transaction>(), "0");
            CreateNewBlock(proof: 513836, hash: "00000a712adde5f716eec160bef8488a1a17eda30b3734a23efca8eee81d408c", previousHash: "0"); //genesis block
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

                //Original Check that the Proof of Work is correct
                //if (!IsValidProof(lastBlock.Proof, block.Proof, lastBlock.PreviousHash))
                //    return false;

                //Alaa Check that the proof of Work is correct
                if (!IsValidProof(block, lastBlock.PreviousHash))
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

        private Block CreateNewBlock(int proof, string hash,string previousHash = null)
        {
            var block = new Block
            {
                Index = _chain.Count,
                Timestamp = DateTime.Now,
                Transactions = _currentTransactions.ToList(),
                Nounce = proof,
                PreviousHash = previousHash ?? GetHash(_chain.Last())
            };
            block.Hash = hash;
            _currentTransactions.Clear();
            _chain.Add(block);
            return block;
        }

        //Original Proof of Work
        //private int CreateProofOfWork(int lastProof, string previousHash)
        //{
        //    int proof = 0;
        //    while (!IsValidProof(lastProof, proof, previousHash))
        //        proof++;

        //    return proof;
        //}

        //Alaa Proof of Work
        private Tuple<int, string> ProofOfWork(List<Transaction> data, string previousHash)
        {
            int nounce = 0;
            int showNounceCounter = 0;
            bool isSuccess;
            string hash;
            do
            {
                if (cancelMining)
                {
                    cancelMining = false;
                    return new Tuple<int, string>(-1,"canceled");
                }

                showNounceCounter++;
                var dataText = JsonConvert.SerializeObject(data);
                string guess = $"{dataText}{nounce}{previousHash}";
                 hash = GetSha256(guess);
                if (showNounceCounter == 100)
                {
                    OnShowCurrentNounce(new ShowNounceEventArgs(nounce.ToString()));

                    showNounceCounter = 0;
                }
                 isSuccess = hash.StartsWith(difficultyLevel);
                if (!isSuccess)
                    nounce++;
            } while (!isSuccess);
            OnShowCurrentNounce(new ShowNounceEventArgs(nounce.ToString()));
            return  new Tuple<int, string>(nounce, hash);
        }


        //Alaa  Proof Check
        private bool IsValidProof(Block block,string previousHash)
        {
            var dataText = JsonConvert.SerializeObject(block.Transactions);
            string guess = $"{dataText}{block.Nounce}{previousHash}";
            var hash = GetSha256(guess);
            return hash.StartsWith(difficultyLevel);
        }
        //Original Proof Check
        //private bool IsValidProof(int lastProof, int proof, string previousHash)
        //{
        //    string guess = $"{lastProof}{proof}{previousHash}";
        //    string result = GetSha256(guess);
        //    return result.StartsWith("0000");
        //}

        //Original GetHash
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
        internal void Mine()
        {
            //int proof = CreateProofOfWork(_lastBlock.Proof, _lastBlock.PreviousHash);

            //Alaa Proof of work
            Tuple<int, string> powResult = new Tuple<int, string>(0,String.Empty);
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate
            {
                //Block Reward
                CreateTransaction(sender: "Reward", recipient: NodeId, amount: 15);

                powResult = ProofOfWork(_currentTransactions, _lastBlock.PreviousHash);
                cancelMining = false;
            };
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += (sender, args) =>
            {

                if (powResult.Item1 != -1)
                {
                    //Alaa New Blocks
                    Block block = CreateNewBlock(powResult.Item1, powResult.Item2);

                    //Send New Block Mined Event
                    SendNewBlockMinedEvent();

                    OnNewBlockMined();
                }
                else
                {
                    
                    OnBlockMiningCanceled();

                }
                

            };

            //Block block = CreateNewBlock(proof /*, _lastBlock.PreviousHash*/);

            //    var response = new
            //{
            //    Message = "New Block Forged",
            //    Index = block.Index,
            //    Transactions = block.Transactions.ToArray(),
            //    Proof = block.Proof,
            //    PreviousHash = block.PreviousHash
            //};

            //return JsonConvert.SerializeObject(response);
        }

        private void SendNewBlockMinedEvent()
        {
            foreach (Node node in _nodes)
            {
                var url = new Uri(node.Address, "/consensus/request");
                var request = (HttpWebRequest) WebRequest.Create(url);
                HttpWebResponse res;
                try
                {
                    res = (HttpWebResponse) request.GetResponse();
                }
                catch
                {
                    continue;
                }
            }
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

        internal void RequestUpdate()
        {
            cancelMining = true;

            _currentTransactions.Clear();

            OnConsensusValidateRequest();

            Consensus();
        }
        internal string Consensus()
        {
            cancelMining = false;
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



        protected virtual void OnNewBlockMined()
        {
            NewBlockMined?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnConsensusValidateRequest()
        {
            ConsensusValidateRequest?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnShowCurrentNounce(ShowNounceEventArgs eventargs)
        {
            ShowCurrentNounce?.Invoke(this, eventargs);
        }

        protected virtual void OnBlockMiningCanceled()
        {
            BlockMiningCanceled?.Invoke(this, EventArgs.Empty);
        }
    }
}
