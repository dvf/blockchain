using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BlockChain.Domain.Model
{
    public class Block : ValueObject
    {
        private readonly long index;
        private readonly long timestamp;
        private readonly ProofOfWork proof;
        private readonly Sha256Hash previousHash;
        private readonly ICollection<Transaction> transactions;

        public Block(long index, ProofOfWork proof, Sha256Hash previousHash, IEnumerable<Transaction> transactions, long? timestamp = null)
        {
            if (index <= 0)
                throw new ArgumentException("Index must be greater than zero");
            if (proof == null)
                throw new ArgumentNullException("Proof must not be null");
            if (previousHash == null)
                throw new ArgumentNullException("PreviousHash must not be null");
            if (timestamp.HasValue && timestamp.Value <= 0)
                throw new ArgumentException("Timestamp must be greater than zero");

            // Blocks without transactions are adding new layers of difficulty to ensure the
            // practical immutability of past blocks, i.e. the list of transactions may be empty

            this.index = index;
            this.timestamp = timestamp ?? DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.proof = proof;
            this.previousHash = previousHash;
            this.transactions = transactions.ToList();
        }

        private Block()
        {
            this.index = 1;
            this.timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            this.proof =  new ProofOfWork(1);
            this.previousHash =  Sha256Hash.Of("Genesis");
            this.transactions = new List<Transaction>();
        }

        public long Index => index;

        public long Timestamp => timestamp;

        [JsonConverter(typeof(ObjectToPropertyConverter), typeof(ProofOfWork), "Value")]
        public ProofOfWork Proof => proof;

        [JsonConverter(typeof(ObjectToPropertyConverter), typeof(Sha256Hash), "Value")]
        public Sha256Hash PreviousHash => previousHash;

        public IReadOnlyCollection<Transaction> Transactions => transactions.AsReadOnly();

        public static Block Genesis()
        {
            return new Block();
        }

        public static Block Reconstitute(long index, long timestamp, long proof, string previousHash, IEnumerable<Transaction> transactions)
        {
            return new Block(index, new ProofOfWork(proof), new Sha256Hash(previousHash), transactions, timestamp);
        }

        public bool IsGenesisBlock()
        {
            return Index == 1 
                && PreviousHash == Sha256Hash.Of("Genesis")
                && Proof == new ProofOfWork(1);
        }

        public Sha256Hash Hash()
        {
            var properties = new {
                Index,
                Timestamp, 
                Proof, 
                PreviousHash, 
                Transactions};
            return Sha256Hash.Of(properties.AsJson());
        }

        public bool VerifyProofOfWork(Block other)
        {
            return Proof.Verify(other.Proof);
        }
    }
}