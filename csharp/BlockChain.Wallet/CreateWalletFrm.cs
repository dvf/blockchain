using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Secp256k1Net;


namespace BlockChain.Wallet
{
    public partial class CreateWalletFrm : Form
    {
        private WalletHelper.KeyPair _keyPair;
        public CreateWalletFrm()
        {
            InitializeComponent();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _keyPair.SaveToFile(saveFileDialog1.FileName);
                MessageBox.Show("Save Successful!");
            }

        }

        private void generateBtn_Click(object sender, EventArgs e)
        {
            var s = new Secp256k1();
            _keyPair = WalletHelper.GenerateKeyPair(s);
            privateKeyTxt.Text = _keyPair.PrivateKey;
            publicKeyTxt.Text = _keyPair.PublicKey;
        }
    }
}
