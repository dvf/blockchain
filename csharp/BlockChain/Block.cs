using System;
using System.Collections.Generic;

namespace BlockChainDemo
{
    public class Block
    {
        public int Index { get; set; }
        public string Hash { get; set; }
        public DateTime Timestamp { get; set; }
        public List<Transaction> Transactions { get; set; }
        public int Nounce { get; set; }
        public string PreviousHash { get; set; }
        public string MindedBy { get; set; }

        public override string ToString()
        {
            return $"{Index} [{Timestamp.ToString("yyyy-MM-dd HH:mm:ss")}] Proof: {Nounce} | PrevHash: {PreviousHash} | Trx: {Transactions.Count}";
        }
    }
}