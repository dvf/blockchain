using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockChainDemo;

namespace BlockChain.Wallet
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void initiateControls()
        {
            //connectBtn.Enabled = false;
            queryBalanceBtn.Enabled = true;
            sendBtn.Enabled = true;
        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            nodeCmb.SelectedIndex = 0;
            senderTxt.Text = Program.ContextKeyPair.PublicKey;

        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            Program.Url = nodeCmb.Text;
            initiateControls();
        }

        private void queryBalanceBtn_Click(object sender, EventArgs e)
        {
            var balance = NodeApiClient.QueryBalance(Program.ContextKeyPair.PublicKey);
            balanceTxt.Text = balance;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var transaction = new Transaction();
            transaction.Sender = senderTxt.Text;
            transaction.Recipient = receiverTxt.Text;
            transaction.Amount = (int)amountTxt.Value;
            transaction.TimeStamp = DateTime.Now;
            transaction.Sign(Program.ContextKeyPair.PrivateKey);
            var message = NodeApiClient.SendTransaction(transaction);
            MessageBox.Show(message);
        }
    }
}
