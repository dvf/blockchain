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

namespace BlockChain.Miner
{
    public partial class EditTransactionFrm : Form
    {
        private readonly Transaction _transaction;

        public EditTransactionFrm(Transaction transaction)
        {
            _transaction = transaction;
            InitializeComponent();
        }

        private void EditTransactionFrm_Load(object sender, EventArgs e)
        {
            senderTXT.Text = _transaction.Sender;
            receiverTxt.Text = _transaction.Recipient;
            amountTxt.Value = _transaction.Amount;
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            _transaction.Sender = senderTXT.Text;
            _transaction.Recipient = receiverTxt.Text;
            _transaction.Amount = (int)amountTxt.Value;
            var message = NodeApiClient.EditTransaction(_transaction);
            MessageBox.Show(message);
        }
    }
}
