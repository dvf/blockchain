using System;

namespace BlockChain.Infrastructure.DTO
{
    public class Transaction
    {
        public Guid? Sender { get; set; }
        public Guid Recipient { get; set; }
        public long Amount { get; set; }
    }
}