using System.Collections.Generic;

namespace BlockChain.Infrastructure.DTO
{
    public class Block
    {
        public long Index { get; set; }
        public long Timestamp { get; set; }
        public long Proof { get; set; }
        public string PreviousHash { get; set; }
        public List<Transaction> Transactions { get; set; }
    }
}