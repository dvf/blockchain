using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockChain.Wallet
{
    static class Program
    {
        public static string Url { get; set; }
        public static WalletHelper.KeyPair ContextKeyPair { get; set; }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new UnlockFrm());
        }
    }
}
