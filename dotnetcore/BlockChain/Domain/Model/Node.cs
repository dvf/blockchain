using System;

namespace BlockChain.Domain.Model
{
    public class Node : ValueObject
    {
        private readonly Uri address;

        public Node(Uri address) 
        {
            this.address = address;
        }

        public Node(string address)
         : this(new Uri(address))
        {
        }
        
        public Uri Address => address;

        public static Node Default()
        {
            return new Node(@"http://127.0.0.1:5000");
        }
    }
}