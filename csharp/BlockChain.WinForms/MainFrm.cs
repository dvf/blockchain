using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockChain.WinForms.Properties;
using BlockChainDemo;

namespace BlockChain.WinForms
{
    public partial class MainFrm : Form
    {
        private WebServer _server;
        private dynamic _data;
        private dynamic _transactions;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            LoadWebServerSettings();
            var chain = new BlockChainDemo.BlockChain();
            
            _server = new WebServer(chain, WebServerSettings.Default.Host, 
                WebServerSettings.Default.Port);
        }

        private void LoadWebServerSettings()
        {
            hostTxtBox.Text = WebServerSettings.Default.Host;
            portTxtBox.Text = WebServerSettings.Default.Port;
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e)
        {
            WebServerSettings.Default.Host = hostTxtBox.Text;
            WebServerSettings.Default.Port = portTxtBox.Text;
            WebServerSettings.Default.Save();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            try
            {
                _server.Start();
                urlTxtBox.Text = _server.Url;
                initiateControlForStart();
            }
            catch (Exception ex)
            {

                urlTxtBox.Text = ex.Message;
              
            }
        }

        private void initiateControlForStart()
        {
            startBtn.Enabled = false;
            loadChainBtn.Enabled = true;
            sendBtn.Enabled = true;
            mineBtn.Enabled = true;
            validateBtn.Enabled = true;
        }
        private void loadChainBtn_Click(object sender, EventArgs e)
        {
            _data = NodeApiClient.LoadBlockChain();
            blocksGrid.DataSource = _data.chain;
        }

        private void blocksGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex != 3)
                return;
            _transactions = _data.chain[e.RowIndex];
            transGrd.DataSource = _transactions["Transactions"];
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var result = NodeApiClient.SendTransaction(new Transaction()
            {
                Amount = (int) amountTxt.Value,
                Sender = senderTxt.Text,
                Recipient = receiverTxt.Text
            });
            MessageBox.Show(result);
        }

        private void mineBtn_Click(object sender, EventArgs e)
        {
            var result = NodeApiClient.MineBlock();
            MessageBox.Show(result);
        }
    }
}
