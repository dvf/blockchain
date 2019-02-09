using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Secp256k1Net;

namespace BlockChain.Wallet
{
    class WalletHelper
    {
        public class KeyPair
        {
            public string PrivateKey;
            public string PublicKey;

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }

            public void Deserialize(string text)
            {
                var result = JsonConvert.DeserializeObject<KeyPair>(text);
                this.PrivateKey = result.PrivateKey;
                this.PublicKey = result.PublicKey;
            }

            public void SaveToFile(string path)
            {
                using (var stream = new StreamWriter(path))
                {
                    stream.Write(this.Serialize());
                }
            }

            public void LoadFromFile(string path)
            {
                using (var stream = new StreamReader(path))
                {
                    var text = stream.ReadToEnd();
                    Deserialize(text);
                }
            }
        }

        public static byte[] GeneratePrivateKey(Secp256k1 secp256k1)
        {
            var rnd = RandomNumberGenerator.Create();
            byte[] privateKey = new byte[32];
            do
            {
                rnd.GetBytes(privateKey);
            }
            while (!secp256k1.SecretKeyVerify(privateKey));
            return privateKey;
        }

        public static KeyPair GenerateKeyPair(Secp256k1 secp256k1)
        {
            var privateKey = GeneratePrivateKey(secp256k1);
            byte[] publicKey = new byte[64];
            if (!secp256k1.PublicKeyCreate(publicKey, privateKey))
            {
                throw new Exception("Public key creation failed");
            }
            return new KeyPair { PrivateKey = Convert.ToBase64String( privateKey),  PublicKey =  Convert.ToBase64String(publicKey) };
        }

    }
}
