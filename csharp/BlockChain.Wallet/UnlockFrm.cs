using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockChain.Wallet
{
    public partial class UnlockFrm : Form
    {
        public UnlockFrm()
        {
            InitializeComponent();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(privateKeyTxt.Text))
            {
                MessageBox.Show("Key pair must be selected first!");
                return;
            }
            if (Program.ContextKeyPair != null)
            {
                var frm = new MainFrm();
                frm.Show();
                this.Hide();
            }
        }

        private void createWalletBtn_Click(object sender, EventArgs e)
        {
            var frm = new CreateWalletFrm();
            frm.ShowDialog();
        }

        private void openBtn_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                privateKeyTxt.Text = openFileDialog1.FileName;
                try
                {
                    Program.ContextKeyPair = new WalletHelper.KeyPair();
                    Program.ContextKeyPair.LoadFromFile(privateKeyTxt.Text);
                    walletAddressTxt.Text = Program.ContextKeyPair.PublicKey;
                }
                catch (Exception exception)
                {
                    Program.ContextKeyPair = null;
                    MessageBox.Show("key pair is not invalid!");
                    return;
                }
            }
        }
    }
}
