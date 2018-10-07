using System;
using Newtonsoft.Json;

namespace BlockChain.Domain.Model
{
    public class Transaction : ValueObject
    {
        private readonly Account sender;
        private readonly Account recipient;
        private readonly long amount;

        public Transaction(Account sender, Account recipient, long amount)
        {
            if (recipient == null)
                throw new ArgumentNullException("Recipient must not be null");
            if (amount <= 0)
                throw new ArgumentException("Amount must not be negative or zero");

            this.sender = sender;
            this.recipient = recipient;
            this.amount = amount;
        }

        public Transaction(Guid? sender, Guid recipient, long amount)
        {
            if (sender == Guid.Empty)
                throw new ArgumentException("Sender must not be empty");
            if (recipient == default)
                throw new ArgumentNullException("Recipient must not be empty");
            if (amount <= 0)
                throw new ArgumentException("Amount must not be negative or zero");

            if (sender != null)
            {
                this.sender = Account.Of(sender.Value);
            }
            this.recipient = Account.Of(recipient);
            this.amount = amount;
        }

        [JsonConverter(typeof(ObjectToPropertyConverter), typeof(Account), "Address")]
        public Account Sender => sender;

        [JsonConverter(typeof(ObjectToPropertyConverter), typeof(Account), "Address")]
        public Account Recipient => recipient;

        public long Amount => amount;
    }
}