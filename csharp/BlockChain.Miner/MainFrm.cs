using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using BlockChain.Miner.Properties;
using BlockChainDemo;

namespace BlockChain.Miner
{
    public partial class MainFrm : Form
    {
        private WebServer _server;
        private static bool _autoMining;
        private dynamic _data;
        private dynamic _transactions;
        public MainFrm()
        {
            InitializeComponent();
        }

        private void LoadWebServerSettings()
        {
            hostTxtBox.Text = WebServerSettings.Default.Host;
            portTxtBox.Text = WebServerSettings.Default.Port;
            blockTimeTxt.Value = WebServerSettings.Default.BlockTime;
            blockTimeSettingTxt.Value = WebServerSettings.Default.BlockTime;
            nodeNameTxt.Text = WebServerSettings.Default.NodeName;
            senderTxt.Text = WebServerSettings.Default.NodeName;
        }

        delegate void LogArgReturningVoidDelegate(string text, Severity severity);
        private void Log(string message,Severity severity=Severity.Info)
        {

            if (this.logTxt.InvokeRequired)
            {
                LogArgReturningVoidDelegate d = new LogArgReturningVoidDelegate(Log);
                this.logTxt.BeginInvoke(d, new object[] { message,severity });
            }
            else
            {
                logTxt.SelectionStart = logTxt.TextLength;
                logTxt.SelectionLength = 0;
                switch (severity)
                {
                    case Severity.Info:
                        logTxt.SelectionColor = Color.Black;
                        break;
                    case Severity.Warning:
                        logTxt.SelectionColor = Color.Chocolate;
                        break;
                    case Severity.Error:
                        logTxt.SelectionColor = Color.Red;
                        break;
                    case Severity.Success:
                        logTxt.SelectionColor = Color.Green;
                        break;
                }
                var logMessage = string.Empty;
                logMessage += $"-------------------{DateTime.Now}------------------\r\n";
                logMessage += $"{message}\r\n";
                logMessage += ("------------------------------------------------------\r\n");
                this.logTxt.AppendText(logMessage);
                logTxt.SelectionColor = logTxt.ForeColor;
            }
        }
        private void ChainOnBlockMiningCanceled(object sender, EventArgs eventArgs)
        {
            Log("Current Block Mining Canceled",Severity.Warning);

            if (_autoMining)
            {
                MineANewBlock();
            }
        }

        private void ChainOnNewBlockMined(object sender, EventArgs eventArgs)
        {
            Log("A new block has been mined",Severity.Success);

            if (_autoMining)
            {
                MineANewBlock();
            }
        }

        private void HoldForBlockTime()
        {
            while ((DateTime.Now.Second % WebServerSettings.Default.BlockTime) != 0)
            {
                Thread.Sleep(500);
                Application.DoEvents();
            }
        }
        private void MineANewBlock(bool holdForBlockTime = true)
        {
            if (holdForBlockTime)
            {
                HoldForBlockTime();
            }
            Log("Start mining a new Block");
            NodeApiClient.MineBlock();

        }

        delegate void StringArgReturningVoidDelegate(string texty);
        private void ShowNounce(string nounce)
        {
            if (this.nounceTxt.InvokeRequired)
            {
                StringArgReturningVoidDelegate d = new StringArgReturningVoidDelegate(ShowNounce);
                this.nounceTxt.BeginInvoke(d, new object[] { nounce });
            }
            else
            {
                nounceTxt.Text = nounce;
            }
        }

        private void ChainOnShowCurrentNounce(object sender, ShowNounceEventArgs eventArgs)
        {
            ShowNounce(eventArgs.Message);
        }

        private void ChainOnConsensusValidateRequest(object sender, EventArgs eventArgs)
        {
            Log("A consensus update is request has been received");
            var validateResult = NodeApiClient.ValidateConsensus();
            Severity severity = Severity.Info;
            if (validateResult.Contains("authoritive"))
            {
                severity = Severity.Success;
            }
            else
            {
                severity = Severity.Warning;
            }
            Log(validateResult,severity);

        }
        private void MainFrm_Load(object sender, EventArgs e)
        {
            LoadWebServerSettings();
            var chain = new BlockChainDemo.BlockChain(WebServerSettings.Default.NodeName);

            Text += $" Node : {WebServerSettings.Default.NodeName}";
            chain.NewBlockMined += ChainOnNewBlockMined;
            chain.BlockMiningCanceled += ChainOnBlockMiningCanceled;
            chain.ConsensusValidateRequest += ChainOnConsensusValidateRequest;
            chain.ShowCurrentNounce += ChainOnShowCurrentNounce;
            chain.NotifyBadTransaction += ChainOnNotifyBadTransaction;
            _server = new WebServer(chain, WebServerSettings.Default.Host,
                WebServerSettings.Default.Port);
        }

        private void ChainOnNotifyBadTransaction(object sender, BadTransactionEventArgs eventArgs)
        {
            Log($"Transaction from: {eventArgs.Transaction.Sender} to {eventArgs.Transaction.Recipient} for {eventArgs.Transaction.Amount} is bad transaction", Severity.Error);
        }

        private void initiateControlForStart()
        {
            startBtn.Enabled = false;
            loadChainBtn.Enabled = true;
            mineBtn.Enabled = true;
            validateBtn.Enabled = true;
            addPeerBtn.Enabled = true;
            autoMineBtn.Enabled = true;
            queryBalanceBtn.Enabled = true;
            loadPeersBtn.Enabled = true;
            sendBtn.Enabled = true;
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

        private void loadChainBtn_Click(object sender, EventArgs e)
        {
            _data = NodeApiClient.LoadBlockChain();

            blocksGrid.DataSource = null;
            blocksGrid.DataSource = _data.chain;

            //set autosize mode
            blocksGrid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            blocksGrid.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            blocksGrid.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }

        private void mineBtn_Click(object sender, EventArgs e)
        {
            MineANewBlock(false);
        }

        private void autoMineBtn_Click(object sender, EventArgs e)
        {
            _autoMining = true;
            mineBtn.Enabled = false;
            autoMineBtn.Enabled = false;

            MineANewBlock();

        }

        private void validateBtn_Click(object sender, EventArgs e)
        {
            var message = NodeApiClient.ValidateConsensus();
            Severity severity = Severity.Info;
            if (message.Contains("authoritive"))
            {
                severity = Severity.Success;
            }
            else
            {
                severity = Severity.Warning;
            }
            Log(message,severity);
        }

        private void saveSettingsBtn_Click(object sender, EventArgs e)
        {
            WebServerSettings.Default.Host = hostTxtBox.Text;
            WebServerSettings.Default.Port = portTxtBox.Text;
            WebServerSettings.Default.BlockTime = (int)blockTimeSettingTxt.Value;
            WebServerSettings.Default.NodeName = nodeNameTxt.Text;
            WebServerSettings.Default.Save();
        }

        private void loadPeersBtn_Click(object sender, EventArgs e)
        {
            var peersFile = new StreamReader(Application.StartupPath + "\\peers.txt");
            var peersTxt = peersFile.ReadToEnd();
            var peers = peersTxt.Split(new[] {'\r'});
            foreach (var peer in peers)
            {
                try
                {
                    NodeApiClient.AddPeer(peer.Trim());
                    Log($"peer {peer} added to peer list",Severity.Success);
                }
                catch (Exception exception)
                {

                    Log(exception.Message,Severity.Error);
                }

                peersLst.Items.Add(peer);
            }
        }

        private void blocksGrid_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            _transactions = _data.chain[e.RowIndex];
            var showTransactionsFrm = new ShowTransactionsDialog(_transactions);
            showTransactionsFrm.ShowDialog();
        }

        private void queryBalanceBtn_Click(object sender, EventArgs e)
        {
            var balance = NodeApiClient.QueryBalance();
            balanceTxt.Text = balance;
        }

        private void sendBtn_Click(object sender, EventArgs e)
        {
            var result = NodeApiClient.SendTransaction(new Transaction()
            {
                Amount = (int)amountTxt.Value,
                Sender = senderTxt.Text,
                Recipient = receiverTxt.Text
            });
            Log(result,Severity.Success);
        }
    }
}
