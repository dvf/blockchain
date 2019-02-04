using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlockChain.Miner
{
    public partial class ShowTransactionsDialog : Form
    {
        private readonly dynamic _transactions;

        public ShowTransactionsDialog(dynamic transactions)
        {
            _transactions = transactions;
           
            InitializeComponent();
        }

        private void ShowTransactionsDialog_Load(object sender, EventArgs e)
        {
            transGrd.DataSource = _transactions["Transactions"];
        }
    }
}
