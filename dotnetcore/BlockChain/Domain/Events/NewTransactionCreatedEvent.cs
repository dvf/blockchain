using BlockChain.Domain.Model;

namespace BlockChain.Domain.Events
{
    internal class NewTransactionCreatedEvent : IDomainEvent
    {
        private readonly Account sender;
        private readonly Account recipient;
        private readonly long amount;

        public NewTransactionCreatedEvent(Account sender, Account recipient, long amount)
        {
            this.sender = sender;
            this.recipient = recipient;
            this.amount = amount;
        }
    }
}