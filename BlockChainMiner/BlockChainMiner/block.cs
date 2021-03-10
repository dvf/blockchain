using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockChainMiner
{
    public class Block
    {
        public int Index { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int Proof { get; set; }
        public string PreviousHash { get; set; }

        public override string ToString()
        {
            return $"{Index} [{Timestamp.ToString("yyyy-MM-dd HH:mm:ss")}] Proof: {Proof} | PrevHash: {PreviousHash} | Trx: {Transactions.Count}";
        }
    }
    public class Transaction
    {
        public int Amount { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }
    }
    public class Worker
    {
        public int lastindex;
        public int lastProof;
        public string lastHash;

        public Worker()
        {
            lastindex = 0;
            lastProof = 0;
            lastHash = "0";
        }

        public bool Test(int a)
        {
            return lastHash == a.ToString() && lastProof == 0;
        }
    }
    public class HashFound
    {
        public int index;
        public int proof;
        public string adress;
    }
    public class Difficulty
    {
        public int difficulty;
        public int blockTime;
    }
}
