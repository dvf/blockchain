using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Remoting.Messaging;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

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

    public class BadTransactionEventArgs : EventArgs
    {
        public Transaction Transaction { get; set; }
        public BadTransactionEventArgs(Transaction transaction)
        {
            Transaction = transaction;
        }
    }

    public class BadBlockchainEventArgs : EventArgs
    {
        private readonly Block _block;
        private readonly string _message;

        public BadBlockchainEventArgs(Block block,string message)
        {
            _block = block;
            _message = message;
        }
    }

    public class BlockChain
    {
        private List<Transaction> _currentTransactions = new List<Transaction>();
        private List<Transaction> _memPoolTransactions = new List<Transaction>();
        private List<Block> _chain = new List<Block>();
        private List<Node> _nodes = new List<Node>();
        private Block _lastBlock => _chain.Last();
        private string difficultyLevel = "0000";
        private string _nodeName;
        private readonly string _walletAddress;
        private static bool cancelMining = false;

        public event EventHandler NewBlockMined;

        public event EventHandler BlockMiningCanceled;

        public event EventHandler ConsensusValidateRequest;

        public event EventHandler BadBlockchain;

        public delegate void ShowCurrentNounceEventHandler(object sender, ShowNounceEventArgs eventArgs);

        public event ShowCurrentNounceEventHandler ShowCurrentNounce;

        public delegate void NotifyBadTransactionEventHandler(object sender, BadTransactionEventArgs eventArgs);

        public event NotifyBadTransactionEventHandler NotifyBadTransaction;

        public delegate void NotifyBadBlockchainEventHandler(object sender, BadBlockchainEventArgs eventArgs);

        public event NotifyBadBlockchainEventHandler NotifyBadBlockchain;

        public string NodeId { get; private set; }

        //ctor
        public BlockChain(string nodeName,string walletAddress)
        {
            NodeId = Guid.NewGuid().ToString().Replace("-", "");
            _nodeName = nodeName;
            _walletAddress = walletAddress;
            //var gensisProof = ProofOfWork(new List<Transaction>(), "0");
            CreateNewBlock(proof: 513836, hash: "00000a712adde5f716eec160bef8488a1a17eda30b3734a23efca8eee81d408c", previousHash: "0", isGenesis: true); //genesis block
        }

        //private functionality
        private void RegisterNode(string address)
        {
            _nodes.Add(new Node { Address = new Uri(address) });
        }

        private string ComputeBlockHash(Block block)
        {
            var dataText = JsonConvert.SerializeObject(block.Transactions);
            string guess = $"{dataText}{block.Nounce}{block.PreviousHash}";
            var hash = GetSha256(guess);
            return hash;
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
                if (block.PreviousHash != ComputeBlockHash(lastBlock))
                {
                    OnNotifyBadBlockchain(new BadBlockchainEventArgs(block,"Block  hashes is incorrect"));
                    return false;
                }
                foreach (var transaction in block.Transactions)
                {
                    if (!transaction.IsValid())
                    {
                        OnNotifyBadBlockchain(new BadBlockchainEventArgs(block, "Block Transaction Signature is invalid"));
                        return false;
                    }
                }
               

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

        private Block CreateNewBlock(int proof, string hash,string previousHash = null,bool isGenesis = false)
        {
            var minedBy = isGenesis ? "Genesis Block" : _nodeName;
            var block = new Block
            {
                Index = _chain.Count,
                Timestamp = DateTime.Now,
                Transactions = _currentTransactions.ToList(),
                Nounce = proof,
                PreviousHash = previousHash ?? _chain.Last().Hash,
                MindedBy = minedBy
            };
            block.Hash = hash;

            _chain.Add(block);
            return block;
        }

        //Alaa Proof of Work
        private Tuple<int, string> ProofOfWork(string previousHash)
        {
            //Perform Business Logic
            var validTransactions = PerformBusinessLogic();

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
                var dataText = JsonConvert.SerializeObject(validTransactions);
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
            } while (!isSuccess || cancelMining);
            OnShowCurrentNounce(new ShowNounceEventArgs(nounce.ToString()));
            if (!cancelMining)
            {
                return new Tuple<int, string>(nounce, hash);
            }
            else
            {
                return new Tuple<int, string>(-1, "canceled");
            }
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

        private void PublishTransaction(Transaction transaction)
        {
            foreach (Node node in _nodes)
            {
                var client = new RestClient(node.Address);
                var req = new RestRequest("/transactions/add", Method.POST);
                req.AddJsonBody(transaction);
                var response = client.Post<Transaction>(req);
                var message = response.Content;

            }
        }

        private Transaction NewTransaction(string sender, string recipient, int amount)
        {
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Sender = sender,
                Recipient = recipient,
                Amount = amount
            };
            
            _memPoolTransactions.Add(transaction);

            return transaction;

        }

        private bool IsValidTransaction(Transaction transaction)
        {
            try
            {
                if (transaction.Sender == "Coinbase")
                    return true;

                var currentSenderBalance = QueryBalance(transaction.Sender);
                if (currentSenderBalance - transaction.Amount < 0)
                    return false;
                return true;
            }
            catch (Exception exception)
            {

                Debug.WriteLine(exception.Message);
            }
            return false;
        }

        private List<Transaction> PerformBusinessLogic()
        {
            try
            {
                foreach (var currentTransaction in _currentTransactions)
                {
                    if (!IsValidTransaction(currentTransaction))
                    {
                        //Remove Bad Transaction

                        _currentTransactions.RemoveAll(c => c.Id == currentTransaction.Id);


                        //Notify about Bad Transaction
                        OnNotifyBadTransaction(new BadTransactionEventArgs(currentTransaction));

                    }
                }
            }
            catch (Exception exception)
            {

                Debug.WriteLine(exception.Message);
            }
            return _currentTransactions;
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
                NewTransaction(sender: "Coinbase", recipient: _walletAddress, amount: 15);

                //Copy memPool to Current Transactions
                _currentTransactions = new List<Transaction>(_memPoolTransactions.OrderByDescending(t => t.TimeStamp));

                //Clear memPool
                _memPoolTransactions.Clear();

               

                cancelMining = false;

                powResult = ProofOfWork( _lastBlock.Hash);
              
            };
            worker.RunWorkerAsync();
            worker.RunWorkerCompleted += (sender, args) =>
            {

                if (powResult.Item1 != -1)
                {
                    //Alaa New Blocks
                    Block block = CreateNewBlock(powResult.Item1, powResult.Item2);

                    //Clear current transactions
                    _currentTransactions.Clear();

                    //Send New Block Mined Event
                    SendNewBlockMinedEvent();

                    OnNewBlockMined();
                }
                else
                {
                    
                    OnBlockMiningCanceled();

                }
                

            };

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

            //Clear Current MemPool
            _memPoolTransactions.Clear();

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

        internal Transaction AddTransaction(Guid id, string sender, string recipient, int amount,string signature)
        {
            var transaction = new Transaction
            {
                Id = id,
                Sender = sender,
                Recipient = recipient,
                Amount = amount,
                Signature = signature
            };
            if (!transaction.IsValid())
            {
                OnNotifyBadTransaction(new BadTransactionEventArgs(transaction));
                return null;
            }
            else
            {
                _memPoolTransactions.Add(transaction);
            }
            
            return transaction;
        }

        internal int CreateTransaction(Guid id,string sender, string recipient, int amount,string signature)
        {
            if (_memPoolTransactions.All(t => t.Id != id))
            {
                var tr = AddTransaction(id,sender, recipient, amount,signature);
                if (tr != null)
                {
                    PublishTransaction(tr);
                    return _lastBlock != null ? _lastBlock.Index + 1 : 0;
                }
                else
                {
                    return -1;
                }
                
            }
            else
            {
                return -1;
            }
        }

        internal int QueryBalance(string owner)
        {
            var amountSum = 0;
            var amountSub = 0;
            _chain.ForEach(c =>
            {
                foreach (var trans in c.Transactions)
                {
                    if (trans.Recipient == owner)
                    {
                        amountSum += trans.Amount;
                    }
                    if (trans.Sender == owner)
                    {
                        amountSub += trans.Amount;
                    }
                }
            });
            return amountSum - amountSub;
        }

        internal string GetTransactionsMemTool()
        {
            return JsonConvert.SerializeObject(_memPoolTransactions);
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

        protected virtual void OnNotifyBadTransaction(BadTransactionEventArgs eventargs)
        {
            NotifyBadTransaction?.Invoke(this, eventargs);
        }

        protected virtual void OnNotifyBadBlockchain(BadBlockchainEventArgs eventargs)
        {
            NotifyBadBlockchain?.Invoke(this, eventargs);
        }
    }
}
