using System;

namespace BlockChain.ViewModels
{
    public class CreateTransaction
    {
        public Guid Sender{ get; set; }
        public Guid Recipient {get;set;}
        public long Amount {get;set;}
    }
}