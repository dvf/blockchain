using System;

namespace BlockChain.Domain.Events
{
    internal class NodeRegisteredEvent : IDomainEvent
    {
        private readonly Uri address;

        public NodeRegisteredEvent(Uri address)
        {
            this.address = address;
        }
    }
}