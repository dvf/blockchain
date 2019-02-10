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
    public partial class ShowTransactionsDialog : Form
    {
        private readonly List<Transaction> _transactions;

        public ShowTransactionsDialog(List<Transaction> transactions)
        {
            _transactions = transactions;
           
            InitializeComponent();
        }

        private void ShowTransactionsDialog_Load(object sender, EventArgs e)
        {
            //transGrd.DataSource = _transactions["Transactions"];
            transGrd.DataSource = _transactions;
        }

        private void transGrd_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var transaction = _transactions[e.RowIndex];
            var editTransactionFrm = new EditTransactionFrm(transaction);
            editTransactionFrm.ShowDialog();
        }
    }
}
