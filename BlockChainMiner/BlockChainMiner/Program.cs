using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Text;
using System.Net;
using System.Threading;

namespace BlockChainMiner
{

    class Program
    {

        public static string diffstr = "00000";
        public static string nodeadress = "http://localhost:5000";
        public static bool shareSubmitted = true;
        static void Main(string[] args)
        {
            diffstr = "";
            var x = GetDifficulty().difficulty;

            for (int i = 0; i < x; i++)
            {
                diffstr += "0";
            }
            Console.WriteLine("Difficulty :" + x + "G");
            Worker currentWorker = new Worker();
            HashFound HF = new HashFound
            {
                adress = "0ff2498070f34d308b100d43b72f6edd"
            };
            // Start computation.
            while (true)
            {
                if (shareSubmitted)
                {
                    currentWorker = Getwork();
                    shareSubmitted = false;
                }

                // Runtest();
                if (!currentWorker.test(0))
                {
                    HF.index = currentWorker.lastindex;
                    var C = CreateProofOfWork(currentWorker.lastProof, currentWorker.lastHash);
                    Console.WriteLine(C);
                    HF.proof = C;
                    Sumbit(HF);

                }
                /*

                        }

                        /*
                        difficulty = GetDifficulty();
                        string s = "";
                        s = Enumerable.Range(0, 4).ToString();
                        Console.WriteLine(s);*/


            }
        }

       

        static void Miner()
        {
            DateTime x = DateTime.UtcNow;
            Worker currentWorker = new Worker();
            HashFound HF = new HashFound
            {
                adress = "0ff2498070f34d308b100d43b72f6edd"
            };
            if (shareSubmitted)
            {
                currentWorker = Getwork();
                shareSubmitted = false;
            }

            if (!currentWorker.test(0))// if there is a work 
            {
                // we mine it 
                HF.index = currentWorker.lastindex;
                var C = CreateProofOfWork(currentWorker.lastProof, currentWorker.lastHash);
                // if the proof is found
                Console.WriteLine(C);
                HF.proof = C;
                Sumbit(HF);
                // we submit the share
            }

        }
        static Worker Getwork()
        {
            WebRequest request = WebRequest.Create(nodeadress + "/getwork");
            WebResponse rep = request.GetResponse();
            Stream t = rep.GetResponseStream();
            StreamReader IO = new StreamReader(t);

            return JsonConvert.DeserializeObject<Worker>(IO.ReadToEnd());
        }
        static Difficulty GetDifficulty()
        {
            WebRequest request = WebRequest.Create(nodeadress + "/getwork/difficulty");
            WebResponse rep = request.GetResponse();
            Stream t = rep.GetResponseStream();
            StreamReader IO = new StreamReader(t);
            Console.WriteLine();
            return JsonConvert.DeserializeObject<Difficulty>(IO.ReadToEnd());
        }
        public static int CreateProofOfWork(int lastProof, string previousHash)
        {
            int proof = 0;
            var count = 0;
            var t = DateTime.UtcNow;

            while (!IsValidProof(lastProof, proof, previousHash))
            {
                if (t.Second - DateTime.UtcNow.Second + 1 <= 0)
                {
                    t = DateTime.UtcNow;
                    Console.WriteLine($"Speed : {count}/s");
                    count = 0;
                }
                count++;
                proof++;
            }

            return proof;
        }

        private static bool IsValidProof(int lastProof, int proof, string previousHash)
        {
            string guess = $"{lastProof}{proof}{previousHash}";
            string result = GetSha256(guess);
            return result.StartsWith(diffstr);
        }



        private static string GetSha256(string data)
        {
            var sha256 = new SHA256Managed();
            var hashBuilder = new StringBuilder();

            byte[] bytes = Encoding.Unicode.GetBytes(data);
            byte[] hash = sha256.ComputeHash(bytes);

            foreach (byte x in hash)
                hashBuilder.Append($"{x:x2}");

            return hashBuilder.ToString();
        }
        public async static void Sumbit(HashFound HF)
        {
            var json = JsonConvert.SerializeObject(HF);
            using (var client = new HttpClient())
            {
                //to get node adress
                client.BaseAddress = new Uri(nodeadress);
                //serialize your json using newtonsoft json serializer then add it to the StringContent
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                //to add the pervious hash that we found
                var result = await client.PostAsync("/submitwork", content);
                string resultContent = await result.Content.ReadAsStringAsync();
                shareSubmitted = true;
                //if the answer is not blank so we mined a new block
                if (resultContent != "{}") Console.WriteLine("New Block Forged");
            }
        }

    }    
}
