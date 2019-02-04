using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
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
        private static bool autoMining;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            LoadWebServerSettings();
            var chain = new BlockChainDemo.BlockChain();
            Text += $" Node Id : {chain.NodeId}";
            chain.NewBlockMined += ChainOnNewBlockMined;
            chain.BlockMiningCanceled += ChainOnBlockMiningCanceled;
            chain.ConsensusValidateRequest += ChainOnConsensusValidateRequest;
            chain.ShowCurrentNounce += ChainOnShowCurrentNounce;
            _server = new WebServer(chain, WebServerSettings.Default.Host, 
                WebServerSettings.Default.Port);
        }

        private void ChainOnBlockMiningCanceled(object sender, EventArgs eventArgs)
        {
            Log("Current Block Mining Canceled");

            if (autoMining)
            {
                HoldForBlockTime();

                MineANewBlock();
            }
        }

        private void ChainOnNewBlockMined(object sender, EventArgs eventArgs)
        {
            Log("A new block has been mined");

            if (autoMining)
            {
                HoldForBlockTime();
                MineANewBlock();
            }
        }

        private void HoldForBlockTime()
        {
            while ((DateTime.Now.Second % 20) != 0)
            {
                Thread.Sleep(500);
            }
        }
        private void MineANewBlock()
        {
            Log("Start mining a new Block");
            NodeApiClient.MineBlock();

        }

     
        private void ChainOnShowCurrentNounce(object sender, ShowNounceEventArgs eventArgs)
        {
            ShowNounce(eventArgs.Message);
        }

        private void ChainOnConsensusValidateRequest(object sender, EventArgs eventArgs)
        {
            Log("A consensus update is request has been received");
            var validateResult = NodeApiClient.ValidateConsensus();
            Log(validateResult);
            
        }

   

        delegate void StringArgReturningVoidDelegate(string text);
        private void Log(string message)
        {

            if (this.logTxt.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(Log);
                this.logTxt.BeginInvoke(d, new object[] {message});
            }
            else
            {
                var logMessage = string.Empty;
                logMessage += $"-------------------{DateTime.Now}------------------\r\n";
                logMessage += $"{message}\r\n";
                logMessage += ("------------------------------------------------------\r\n");
                this.logTxt.AppendText(logMessage);
            }
        }

        private void ShowNounce(string nounce)
        {
            if (this.nounceTxt.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(ShowNounce);
                this.nounceTxt.BeginInvoke(d, new object[] {nounce});
            }
            else
            {
                nounceTxt.Text = nounce;
            }
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
            addPeerBtn.Enabled = true;
            autoMineBtn.Enabled = true;
        }
        private void loadChainBtn_Click(object sender, EventArgs e)
        {
            _data = NodeApiClient.LoadBlockChain();
            blocksGrid.DataSource = _data.chain;

            //set autosize mode
            blocksGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            blocksGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


        }

        private void blocksGrid_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if(e.ColumnIndex != 3)
                return;
            _transactions = _data.chain[e.RowIndex];
            transGrd.DataSource = _transactions["Transactions"];
            //transGrd.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //transGrd.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //transGrd.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
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
            MineANewBlock();
            //NodeApiClient.MineBlock();
            //var result = NodeApiClient.MineBlock();
            //MessageBox.Show(result);
        }

        private void addPeerBtn_Click(object sender, EventArgs e)
        {
            var peerUrl = peerTxt.Text;
            NodeApiClient.AddPeer(peerUrl);
            peersLst.Items.Add(peerUrl);
        }

        private void validateBtn_Click(object sender, EventArgs e)
        {
            var message = NodeApiClient.ValidateConsensus();
            MessageBox.Show(message);
        }

        private void autoMineBtn_Click(object sender, EventArgs e)
        {
            autoMining = true;
            MineANewBlock();

            mineBtn.Enabled = false;
            autoMineBtn.Enabled = false;
            //autoMineBtn.Enabled = false;
            //autoMineTimer.Interval = ((int)blockTimeTxt.Value) * 1000;
            //autoMineTimer.Enabled = true;
        }

        private void autoMineTimer_Tick(object sender, EventArgs e)
        {
            Log("Start mining a new Block");
            NodeApiClient.MineBlock();
          
            //var message = NodeApiClient.ValidateConsensus();
            //logTxt.AppendText($"-------------------{DateTime.Now}------------------\n");
            //logTxt.AppendText($"Consensus Validate Result : {message}\n");
            //logTxt.AppendText("------------------------------------------------------\n");
        }
    }
}
