using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BlockChain.Domain.Model
{
    public class Sha256Hash : ValueObject
    {
        private readonly string hash;

        public Sha256Hash(string hash)
        {
            if (string.IsNullOrEmpty(hash))
                throw new ArgumentException("Previous hash must not be empty");
            if (!Regex.IsMatch(hash, "[A-Fa-f0-9]{64}"))
                throw new ArgumentException("Previous hash must be a valid SHA256 hash");

            this.hash = hash;
        }

        public string Value => hash;

        public bool StartsWith(string value)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException("Value must not be null or empty");

            return hash.StartsWith(value);
        }

        public static Sha256Hash Of(string value)
        {
            var builder = new StringBuilder();
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(value);
                var computedHash = sha256.ComputeHash(bytes);
                foreach (Byte b in computedHash)
                    builder.Append(b.ToString("x2"));
            }
            var hash = builder.ToString();
            return new Sha256Hash(hash);
        }

        public override string ToString()
        {
            return hash;
        }
    }
}