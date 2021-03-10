using System;

namespace BlockChain.Domain.Model
{
    public class Account : ValueObject
    {
        public Account() : this(Guid.NewGuid())
        {
        }

        private Account(Guid address)
        {
            if (address == Guid.Empty)
                throw new ArgumentException("Address must not be empty");
            Address = address;
        }

        public Guid Address { get; }

        public static Account Of(Guid address)
        {
            return new Account(address);
        }
    }
}