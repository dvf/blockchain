using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Secp256k1Net;

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
        public string Signature { get; set; }

        public string ComputeHash()
        {
            var hasher = new SHA256CryptoServiceProvider();
          //  var obj = JsonConvert.SerializeObject(this);
            var obj = $"{Sender}{Recipient}{Amount}";
            var hash = hasher.ComputeHash(Encoding.UTF8.GetBytes(obj));
            return Convert.ToBase64String(hash);
        }
        public void Sign(string privateKey)
        {
            using (var sep = new Secp256k1())
            {
                var secret = Convert.FromBase64String(privateKey);
                var hash = Convert.FromBase64String(ComputeHash());
                var signature = new byte[64];
                sep.Sign(signature, hash, secret);
                Signature = Convert.ToBase64String(signature);
            }
        }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Sender))
                return false;
            if (string.IsNullOrEmpty(Signature))
                throw new Exception("No Signature is found is this transaction");

            using (var sep = new Secp256k1())
            {
                var signature = Convert.FromBase64String(Signature);
                var hash = Convert.FromBase64String(ComputeHash());
                var pk = Convert.FromBase64String(Sender);
                return sep.Verify(signature, hash, pk);
            }
        }

    }
}