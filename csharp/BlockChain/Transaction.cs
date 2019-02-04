using System;

namespace BlockChainDemo
{
    public class Transaction
    {
        public Transaction()
        {
            TimeStamp = DateTime.Now;
        }

        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Recipient { get; set; }
        public string Sender { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}