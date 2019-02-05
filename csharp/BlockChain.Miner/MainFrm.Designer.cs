namespace BlockChain.Miner
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.webServerCtlGrpBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.urlTxtBox = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.consensusGrp = new System.Windows.Forms.GroupBox();
            this.validateBtn = new System.Windows.Forms.Button();
            this.chainExplorerGrpBox = new System.Windows.Forms.GroupBox();
            this.loadChainBtn = new System.Windows.Forms.Button();
            this.blocksGrid = new System.Windows.Forms.DataGridView();
            this.blockMiningGrp = new System.Windows.Forms.GroupBox();
            this.nounceTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.mineBtn = new System.Windows.Forms.Button();
            this.walletTab = new System.Windows.Forms.TabPage();
            this.newTransGrpBox = new System.Windows.Forms.GroupBox();
            this.amountTxt = new System.Windows.Forms.NumericUpDown();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.receiverTxt = new System.Windows.Forms.TextBox();
            this.receiverLbl = new System.Windows.Forms.Label();
            this.senderTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.queryBalanceGrp = new System.Windows.Forms.GroupBox();
            this.balanceTxt = new System.Windows.Forms.TextBox();
            this.queryBalanceBtn = new System.Windows.Forms.Button();
            this.peersTab = new System.Windows.Forms.TabPage();
            this.peersGrp = new System.Windows.Forms.GroupBox();
            this.loadPeersBtn = new System.Windows.Forms.Button();
            this.addPeerBtn = new System.Windows.Forms.Button();
            this.peerTxt = new System.Windows.Forms.TextBox();
            this.peersLst = new System.Windows.Forms.ListBox();
            this.memPoolTab = new System.Windows.Forms.TabPage();
            this.settingsTab = new System.Windows.Forms.TabPage();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.minningSettingGrp = new System.Windows.Forms.GroupBox();
            this.nodeNameTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.blockTimeSettingTxt = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.webServerSettingsGrpBox = new System.Windows.Forms.GroupBox();
            this.portTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.logsGrp = new System.Windows.Forms.GroupBox();
            this.logTxt = new System.Windows.Forms.RichTextBox();
            this.autoMineGrp = new System.Windows.Forms.GroupBox();
            this.autoMineBtn = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.blockTimeTxt = new System.Windows.Forms.NumericUpDown();
            this.memPoolGrp = new System.Windows.Forms.GroupBox();
            this.loadMemPool = new System.Windows.Forms.Button();
            this.memPoolGrd = new System.Windows.Forms.DataGridView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.webServerCtlGrpBox.SuspendLayout();
            this.consensusGrp.SuspendLayout();
            this.chainExplorerGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blocksGrid)).BeginInit();
            this.blockMiningGrp.SuspendLayout();
            this.walletTab.SuspendLayout();
            this.newTransGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).BeginInit();
            this.queryBalanceGrp.SuspendLayout();
            this.peersTab.SuspendLayout();
            this.peersGrp.SuspendLayout();
            this.memPoolTab.SuspendLayout();
            this.settingsTab.SuspendLayout();
            this.minningSettingGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockTimeSettingTxt)).BeginInit();
            this.webServerSettingsGrpBox.SuspendLayout();
            this.logsGrp.SuspendLayout();
            this.autoMineGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockTimeTxt)).BeginInit();
            this.memPoolGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.memPoolGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.walletTab);
            this.tabControl1.Controls.Add(this.peersTab);
            this.tabControl1.Controls.Add(this.memPoolTab);
            this.tabControl1.Controls.Add(this.settingsTab);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(536, 629);
            this.tabControl1.TabIndex = 12;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.webServerCtlGrpBox);
            this.tabPage1.Controls.Add(this.consensusGrp);
            this.tabPage1.Controls.Add(this.chainExplorerGrpBox);
            this.tabPage1.Controls.Add(this.blockMiningGrp);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(528, 603);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox1.Image = global::BlockChain.Miner.Properties.Resources._800_X_600;
            this.pictureBox1.Location = new System.Drawing.Point(187, 112);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(158, 125);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 16;
            this.pictureBox1.TabStop = false;
            // 
            // webServerCtlGrpBox
            // 
            this.webServerCtlGrpBox.Controls.Add(this.label3);
            this.webServerCtlGrpBox.Controls.Add(this.urlTxtBox);
            this.webServerCtlGrpBox.Controls.Add(this.startBtn);
            this.webServerCtlGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.webServerCtlGrpBox.Location = new System.Drawing.Point(6, 6);
            this.webServerCtlGrpBox.Name = "webServerCtlGrpBox";
            this.webServerCtlGrpBox.Size = new System.Drawing.Size(510, 100);
            this.webServerCtlGrpBox.TabIndex = 14;
            this.webServerCtlGrpBox.TabStop = false;
            this.webServerCtlGrpBox.Text = "Node Listener Control";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label3.Location = new System.Drawing.Point(22, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "URL:";
            // 
            // urlTxtBox
            // 
            this.urlTxtBox.Location = new System.Drawing.Point(58, 43);
            this.urlTxtBox.Name = "urlTxtBox";
            this.urlTxtBox.ReadOnly = true;
            this.urlTxtBox.Size = new System.Drawing.Size(192, 20);
            this.urlTxtBox.TabIndex = 1;
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.startBtn.Location = new System.Drawing.Point(264, 41);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // consensusGrp
            // 
            this.consensusGrp.Controls.Add(this.validateBtn);
            this.consensusGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.consensusGrp.Location = new System.Drawing.Point(355, 112);
            this.consensusGrp.Name = "consensusGrp";
            this.consensusGrp.Size = new System.Drawing.Size(161, 125);
            this.consensusGrp.TabIndex = 11;
            this.consensusGrp.TabStop = false;
            this.consensusGrp.Text = "Validate Consensus";
            // 
            // validateBtn
            // 
            this.validateBtn.Enabled = false;
            this.validateBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validateBtn.Location = new System.Drawing.Point(25, 38);
            this.validateBtn.Name = "validateBtn";
            this.validateBtn.Size = new System.Drawing.Size(120, 50);
            this.validateBtn.TabIndex = 0;
            this.validateBtn.Text = "Validate";
            this.validateBtn.UseVisualStyleBackColor = true;
            this.validateBtn.Click += new System.EventHandler(this.validateBtn_Click);
            // 
            // chainExplorerGrpBox
            // 
            this.chainExplorerGrpBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.chainExplorerGrpBox.Controls.Add(this.loadChainBtn);
            this.chainExplorerGrpBox.Controls.Add(this.blocksGrid);
            this.chainExplorerGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.chainExplorerGrpBox.Location = new System.Drawing.Point(9, 243);
            this.chainExplorerGrpBox.Name = "chainExplorerGrpBox";
            this.chainExplorerGrpBox.Size = new System.Drawing.Size(506, 355);
            this.chainExplorerGrpBox.TabIndex = 3;
            this.chainExplorerGrpBox.TabStop = false;
            this.chainExplorerGrpBox.Text = "Blockchain Explorer";
            // 
            // loadChainBtn
            // 
            this.loadChainBtn.Enabled = false;
            this.loadChainBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.loadChainBtn.Location = new System.Drawing.Point(6, 16);
            this.loadChainBtn.Name = "loadChainBtn";
            this.loadChainBtn.Size = new System.Drawing.Size(75, 23);
            this.loadChainBtn.TabIndex = 1;
            this.loadChainBtn.Text = "Load";
            this.loadChainBtn.UseVisualStyleBackColor = true;
            this.loadChainBtn.Click += new System.EventHandler(this.loadChainBtn_Click);
            // 
            // blocksGrid
            // 
            this.blocksGrid.AllowUserToAddRows = false;
            this.blocksGrid.AllowUserToDeleteRows = false;
            this.blocksGrid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.blocksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blocksGrid.Location = new System.Drawing.Point(6, 45);
            this.blocksGrid.Name = "blocksGrid";
            this.blocksGrid.ReadOnly = true;
            this.blocksGrid.Size = new System.Drawing.Size(494, 303);
            this.blocksGrid.TabIndex = 0;
            this.blocksGrid.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.blocksGrid_CellContentDoubleClick);
            // 
            // blockMiningGrp
            // 
            this.blockMiningGrp.Controls.Add(this.nounceTxt);
            this.blockMiningGrp.Controls.Add(this.label7);
            this.blockMiningGrp.Controls.Add(this.mineBtn);
            this.blockMiningGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.blockMiningGrp.Location = new System.Drawing.Point(9, 112);
            this.blockMiningGrp.Name = "blockMiningGrp";
            this.blockMiningGrp.Size = new System.Drawing.Size(160, 125);
            this.blockMiningGrp.TabIndex = 9;
            this.blockMiningGrp.TabStop = false;
            this.blockMiningGrp.Text = "Manual Block Mining";
            // 
            // nounceTxt
            // 
            this.nounceTxt.Location = new System.Drawing.Point(18, 73);
            this.nounceTxt.Name = "nounceTxt";
            this.nounceTxt.ReadOnly = true;
            this.nounceTxt.Size = new System.Drawing.Size(100, 20);
            this.nounceTxt.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label7.Location = new System.Drawing.Point(31, 57);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Current Nounce:";
            // 
            // mineBtn
            // 
            this.mineBtn.Enabled = false;
            this.mineBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineBtn.Location = new System.Drawing.Point(14, 19);
            this.mineBtn.Name = "mineBtn";
            this.mineBtn.Size = new System.Drawing.Size(120, 35);
            this.mineBtn.TabIndex = 0;
            this.mineBtn.Text = "Mine";
            this.mineBtn.UseVisualStyleBackColor = true;
            this.mineBtn.Click += new System.EventHandler(this.mineBtn_Click);
            // 
            // walletTab
            // 
            this.walletTab.Controls.Add(this.newTransGrpBox);
            this.walletTab.Controls.Add(this.queryBalanceGrp);
            this.walletTab.Location = new System.Drawing.Point(4, 22);
            this.walletTab.Name = "walletTab";
            this.walletTab.Padding = new System.Windows.Forms.Padding(3);
            this.walletTab.Size = new System.Drawing.Size(528, 603);
            this.walletTab.TabIndex = 3;
            this.walletTab.Text = "Wallet";
            this.walletTab.UseVisualStyleBackColor = true;
            // 
            // newTransGrpBox
            // 
            this.newTransGrpBox.Controls.Add(this.amountTxt);
            this.newTransGrpBox.Controls.Add(this.sendBtn);
            this.newTransGrpBox.Controls.Add(this.label8);
            this.newTransGrpBox.Controls.Add(this.receiverTxt);
            this.newTransGrpBox.Controls.Add(this.receiverLbl);
            this.newTransGrpBox.Controls.Add(this.senderTxt);
            this.newTransGrpBox.Controls.Add(this.label9);
            this.newTransGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.newTransGrpBox.Location = new System.Drawing.Point(6, 232);
            this.newTransGrpBox.Name = "newTransGrpBox";
            this.newTransGrpBox.Size = new System.Drawing.Size(506, 220);
            this.newTransGrpBox.TabIndex = 17;
            this.newTransGrpBox.TabStop = false;
            this.newTransGrpBox.Text = "New Transaction";
            // 
            // amountTxt
            // 
            this.amountTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.amountTxt.Location = new System.Drawing.Point(58, 71);
            this.amountTxt.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(306, 20);
            this.amountTxt.TabIndex = 6;
            this.amountTxt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // sendBtn
            // 
            this.sendBtn.Enabled = false;
            this.sendBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.sendBtn.Location = new System.Drawing.Point(370, 19);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(130, 72);
            this.sendBtn.TabIndex = 7;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label8.Location = new System.Drawing.Point(9, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(48, 13);
            this.label8.TabIndex = 4;
            this.label8.Text = "Amount:";
            // 
            // receiverTxt
            // 
            this.receiverTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.receiverTxt.Location = new System.Drawing.Point(58, 45);
            this.receiverTxt.Name = "receiverTxt";
            this.receiverTxt.Size = new System.Drawing.Size(306, 20);
            this.receiverTxt.TabIndex = 3;
            // 
            // receiverLbl
            // 
            this.receiverLbl.AutoSize = true;
            this.receiverLbl.Font = new System.Drawing.Font("Tahoma", 8F);
            this.receiverLbl.Location = new System.Drawing.Point(4, 48);
            this.receiverLbl.Name = "receiverLbl";
            this.receiverLbl.Size = new System.Drawing.Size(53, 13);
            this.receiverLbl.TabIndex = 2;
            this.receiverLbl.Text = "Receiver:";
            // 
            // senderTxt
            // 
            this.senderTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.senderTxt.Location = new System.Drawing.Point(58, 19);
            this.senderTxt.Name = "senderTxt";
            this.senderTxt.ReadOnly = true;
            this.senderTxt.Size = new System.Drawing.Size(306, 20);
            this.senderTxt.TabIndex = 1;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label9.Location = new System.Drawing.Point(12, 22);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(45, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Sender:";
            // 
            // queryBalanceGrp
            // 
            this.queryBalanceGrp.Controls.Add(this.balanceTxt);
            this.queryBalanceGrp.Controls.Add(this.queryBalanceBtn);
            this.queryBalanceGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.queryBalanceGrp.Location = new System.Drawing.Point(3, 6);
            this.queryBalanceGrp.Name = "queryBalanceGrp";
            this.queryBalanceGrp.Size = new System.Drawing.Size(509, 220);
            this.queryBalanceGrp.TabIndex = 16;
            this.queryBalanceGrp.TabStop = false;
            this.queryBalanceGrp.Text = "Query Balance";
            // 
            // balanceTxt
            // 
            this.balanceTxt.Location = new System.Drawing.Point(203, 59);
            this.balanceTxt.Name = "balanceTxt";
            this.balanceTxt.ReadOnly = true;
            this.balanceTxt.Size = new System.Drawing.Size(94, 20);
            this.balanceTxt.TabIndex = 8;
            // 
            // queryBalanceBtn
            // 
            this.queryBalanceBtn.Enabled = false;
            this.queryBalanceBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.queryBalanceBtn.Location = new System.Drawing.Point(203, 99);
            this.queryBalanceBtn.Name = "queryBalanceBtn";
            this.queryBalanceBtn.Size = new System.Drawing.Size(94, 42);
            this.queryBalanceBtn.TabIndex = 0;
            this.queryBalanceBtn.Text = "Query Balance";
            this.queryBalanceBtn.UseVisualStyleBackColor = true;
            this.queryBalanceBtn.Click += new System.EventHandler(this.queryBalanceBtn_Click);
            // 
            // peersTab
            // 
            this.peersTab.Controls.Add(this.peersGrp);
            this.peersTab.Location = new System.Drawing.Point(4, 22);
            this.peersTab.Name = "peersTab";
            this.peersTab.Padding = new System.Windows.Forms.Padding(3);
            this.peersTab.Size = new System.Drawing.Size(528, 603);
            this.peersTab.TabIndex = 1;
            this.peersTab.Text = "Peers";
            this.peersTab.UseVisualStyleBackColor = true;
            // 
            // peersGrp
            // 
            this.peersGrp.Controls.Add(this.loadPeersBtn);
            this.peersGrp.Controls.Add(this.addPeerBtn);
            this.peersGrp.Controls.Add(this.peerTxt);
            this.peersGrp.Controls.Add(this.peersLst);
            this.peersGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.peersGrp.Location = new System.Drawing.Point(6, 6);
            this.peersGrp.Name = "peersGrp";
            this.peersGrp.Size = new System.Drawing.Size(499, 172);
            this.peersGrp.TabIndex = 12;
            this.peersGrp.TabStop = false;
            this.peersGrp.Text = "Peers";
            // 
            // loadPeersBtn
            // 
            this.loadPeersBtn.Enabled = false;
            this.loadPeersBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.loadPeersBtn.Location = new System.Drawing.Point(400, 25);
            this.loadPeersBtn.Name = "loadPeersBtn";
            this.loadPeersBtn.Size = new System.Drawing.Size(75, 23);
            this.loadPeersBtn.TabIndex = 5;
            this.loadPeersBtn.Text = "Load Peers";
            this.loadPeersBtn.UseVisualStyleBackColor = true;
            this.loadPeersBtn.Click += new System.EventHandler(this.loadPeersBtn_Click);
            // 
            // addPeerBtn
            // 
            this.addPeerBtn.Enabled = false;
            this.addPeerBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPeerBtn.Location = new System.Drawing.Point(137, 27);
            this.addPeerBtn.Name = "addPeerBtn";
            this.addPeerBtn.Size = new System.Drawing.Size(41, 23);
            this.addPeerBtn.TabIndex = 4;
            this.addPeerBtn.Text = "+";
            this.addPeerBtn.UseVisualStyleBackColor = true;
            // 
            // peerTxt
            // 
            this.peerTxt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peerTxt.Location = new System.Drawing.Point(6, 27);
            this.peerTxt.Name = "peerTxt";
            this.peerTxt.Size = new System.Drawing.Size(124, 21);
            this.peerTxt.TabIndex = 3;
            // 
            // peersLst
            // 
            this.peersLst.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peersLst.FormattingEnabled = true;
            this.peersLst.Location = new System.Drawing.Point(6, 54);
            this.peersLst.Name = "peersLst";
            this.peersLst.Size = new System.Drawing.Size(469, 108);
            this.peersLst.TabIndex = 0;
            // 
            // memPoolTab
            // 
            this.memPoolTab.Controls.Add(this.memPoolGrp);
            this.memPoolTab.Location = new System.Drawing.Point(4, 22);
            this.memPoolTab.Name = "memPoolTab";
            this.memPoolTab.Size = new System.Drawing.Size(528, 603);
            this.memPoolTab.TabIndex = 4;
            this.memPoolTab.Text = "Transactions MemPool";
            this.memPoolTab.UseVisualStyleBackColor = true;
            // 
            // settingsTab
            // 
            this.settingsTab.Controls.Add(this.saveSettingsBtn);
            this.settingsTab.Controls.Add(this.minningSettingGrp);
            this.settingsTab.Controls.Add(this.webServerSettingsGrpBox);
            this.settingsTab.Location = new System.Drawing.Point(4, 22);
            this.settingsTab.Name = "settingsTab";
            this.settingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.settingsTab.Size = new System.Drawing.Size(528, 603);
            this.settingsTab.TabIndex = 2;
            this.settingsTab.Text = "Settings";
            this.settingsTab.UseVisualStyleBackColor = true;
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.saveSettingsBtn.Location = new System.Drawing.Point(441, 8);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(75, 23);
            this.saveSettingsBtn.TabIndex = 4;
            this.saveSettingsBtn.Text = "Save";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.saveSettingsBtn_Click);
            // 
            // minningSettingGrp
            // 
            this.minningSettingGrp.Controls.Add(this.nodeNameTxt);
            this.minningSettingGrp.Controls.Add(this.label4);
            this.minningSettingGrp.Controls.Add(this.blockTimeSettingTxt);
            this.minningSettingGrp.Controls.Add(this.label5);
            this.minningSettingGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.minningSettingGrp.Location = new System.Drawing.Point(9, 143);
            this.minningSettingGrp.Name = "minningSettingGrp";
            this.minningSettingGrp.Size = new System.Drawing.Size(510, 138);
            this.minningSettingGrp.TabIndex = 16;
            this.minningSettingGrp.TabStop = false;
            this.minningSettingGrp.Text = "Mining Settings";
            // 
            // nodeNameTxt
            // 
            this.nodeNameTxt.Location = new System.Drawing.Point(96, 35);
            this.nodeNameTxt.Name = "nodeNameTxt";
            this.nodeNameTxt.Size = new System.Drawing.Size(122, 20);
            this.nodeNameTxt.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label4.Location = new System.Drawing.Point(23, 35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(66, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Node Name:";
            // 
            // blockTimeSettingTxt
            // 
            this.blockTimeSettingTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.blockTimeSettingTxt.Location = new System.Drawing.Point(96, 96);
            this.blockTimeSettingTxt.Name = "blockTimeSettingTxt";
            this.blockTimeSettingTxt.Size = new System.Drawing.Size(120, 20);
            this.blockTimeSettingTxt.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label5.Location = new System.Drawing.Point(20, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "BlockTime:";
            // 
            // webServerSettingsGrpBox
            // 
            this.webServerSettingsGrpBox.Controls.Add(this.portTxtBox);
            this.webServerSettingsGrpBox.Controls.Add(this.label2);
            this.webServerSettingsGrpBox.Controls.Add(this.hostTxtBox);
            this.webServerSettingsGrpBox.Controls.Add(this.label1);
            this.webServerSettingsGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.webServerSettingsGrpBox.Location = new System.Drawing.Point(9, 37);
            this.webServerSettingsGrpBox.Name = "webServerSettingsGrpBox";
            this.webServerSettingsGrpBox.Size = new System.Drawing.Size(510, 100);
            this.webServerSettingsGrpBox.TabIndex = 15;
            this.webServerSettingsGrpBox.TabStop = false;
            this.webServerSettingsGrpBox.Text = "Node Listener Settings";
            // 
            // portTxtBox
            // 
            this.portTxtBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.portTxtBox.Location = new System.Drawing.Point(225, 39);
            this.portTxtBox.Name = "portTxtBox";
            this.portTxtBox.Size = new System.Drawing.Size(100, 20);
            this.portTxtBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label2.Location = new System.Drawing.Point(187, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Port:";
            // 
            // hostTxtBox
            // 
            this.hostTxtBox.Font = new System.Drawing.Font("Tahoma", 8F);
            this.hostTxtBox.Location = new System.Drawing.Point(59, 39);
            this.hostTxtBox.Name = "hostTxtBox";
            this.hostTxtBox.Size = new System.Drawing.Size(122, 20);
            this.hostTxtBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label1.Location = new System.Drawing.Point(20, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Host:";
            // 
            // logsGrp
            // 
            this.logsGrp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.logsGrp.Controls.Add(this.logTxt);
            this.logsGrp.Location = new System.Drawing.Point(16, 648);
            this.logsGrp.Name = "logsGrp";
            this.logsGrp.Size = new System.Drawing.Size(522, 213);
            this.logsGrp.TabIndex = 13;
            this.logsGrp.TabStop = false;
            this.logsGrp.Text = "Logs";
            // 
            // logTxt
            // 
            this.logTxt.BackColor = System.Drawing.Color.Wheat;
            this.logTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.logTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.logTxt.Location = new System.Drawing.Point(3, 16);
            this.logTxt.Name = "logTxt";
            this.logTxt.Size = new System.Drawing.Size(516, 194);
            this.logTxt.TabIndex = 12;
            this.logTxt.Text = "";
            // 
            // autoMineGrp
            // 
            this.autoMineGrp.Controls.Add(this.autoMineBtn);
            this.autoMineGrp.Controls.Add(this.label6);
            this.autoMineGrp.Controls.Add(this.blockTimeTxt);
            this.autoMineGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.autoMineGrp.Location = new System.Drawing.Point(544, 374);
            this.autoMineGrp.Name = "autoMineGrp";
            this.autoMineGrp.Size = new System.Drawing.Size(179, 125);
            this.autoMineGrp.TabIndex = 13;
            this.autoMineGrp.TabStop = false;
            this.autoMineGrp.Text = "Auto Block Mining";
            this.autoMineGrp.Visible = false;
            // 
            // autoMineBtn
            // 
            this.autoMineBtn.Enabled = false;
            this.autoMineBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoMineBtn.Location = new System.Drawing.Point(31, 81);
            this.autoMineBtn.Name = "autoMineBtn";
            this.autoMineBtn.Size = new System.Drawing.Size(120, 33);
            this.autoMineBtn.TabIndex = 5;
            this.autoMineBtn.Text = "Start Auto-Mine";
            this.autoMineBtn.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(28, 25);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "Block Time";
            // 
            // blockTimeTxt
            // 
            this.blockTimeTxt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blockTimeTxt.Location = new System.Drawing.Point(31, 44);
            this.blockTimeTxt.Name = "blockTimeTxt";
            this.blockTimeTxt.Size = new System.Drawing.Size(120, 21);
            this.blockTimeTxt.TabIndex = 3;
            this.blockTimeTxt.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // memPoolGrp
            // 
            this.memPoolGrp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memPoolGrp.Controls.Add(this.loadMemPool);
            this.memPoolGrp.Controls.Add(this.memPoolGrd);
            this.memPoolGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.memPoolGrp.Location = new System.Drawing.Point(12, 3);
            this.memPoolGrp.Name = "memPoolGrp";
            this.memPoolGrp.Size = new System.Drawing.Size(506, 355);
            this.memPoolGrp.TabIndex = 4;
            this.memPoolGrp.TabStop = false;
            this.memPoolGrp.Text = "Transactions Memory Pool";
            // 
            // loadMemPool
            // 
            this.loadMemPool.Enabled = false;
            this.loadMemPool.Font = new System.Drawing.Font("Tahoma", 8F);
            this.loadMemPool.Location = new System.Drawing.Point(6, 16);
            this.loadMemPool.Name = "loadMemPool";
            this.loadMemPool.Size = new System.Drawing.Size(75, 23);
            this.loadMemPool.TabIndex = 1;
            this.loadMemPool.Text = "Load";
            this.loadMemPool.UseVisualStyleBackColor = true;
            this.loadMemPool.Click += new System.EventHandler(this.loadMemPool_Click);
            // 
            // memPoolGrd
            // 
            this.memPoolGrd.AllowUserToAddRows = false;
            this.memPoolGrd.AllowUserToDeleteRows = false;
            this.memPoolGrd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.memPoolGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.memPoolGrd.Location = new System.Drawing.Point(6, 45);
            this.memPoolGrd.Name = "memPoolGrd";
            this.memPoolGrd.ReadOnly = true;
            this.memPoolGrd.Size = new System.Drawing.Size(494, 303);
            this.memPoolGrd.TabIndex = 0;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(546, 873);
            this.Controls.Add(this.autoMineGrp);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.logsGrp);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Blockchain Miner";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.webServerCtlGrpBox.ResumeLayout(false);
            this.webServerCtlGrpBox.PerformLayout();
            this.consensusGrp.ResumeLayout(false);
            this.chainExplorerGrpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blocksGrid)).EndInit();
            this.blockMiningGrp.ResumeLayout(false);
            this.blockMiningGrp.PerformLayout();
            this.walletTab.ResumeLayout(false);
            this.newTransGrpBox.ResumeLayout(false);
            this.newTransGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).EndInit();
            this.queryBalanceGrp.ResumeLayout(false);
            this.queryBalanceGrp.PerformLayout();
            this.peersTab.ResumeLayout(false);
            this.peersGrp.ResumeLayout(false);
            this.peersGrp.PerformLayout();
            this.memPoolTab.ResumeLayout(false);
            this.settingsTab.ResumeLayout(false);
            this.minningSettingGrp.ResumeLayout(false);
            this.minningSettingGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockTimeSettingTxt)).EndInit();
            this.webServerSettingsGrpBox.ResumeLayout(false);
            this.webServerSettingsGrpBox.PerformLayout();
            this.logsGrp.ResumeLayout(false);
            this.autoMineGrp.ResumeLayout(false);
            this.autoMineGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockTimeTxt)).EndInit();
            this.memPoolGrp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.memPoolGrd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage peersTab;
        private System.Windows.Forms.GroupBox peersGrp;
        private System.Windows.Forms.ListBox peersLst;
        private System.Windows.Forms.TabPage settingsTab;
        private System.Windows.Forms.Button addPeerBtn;
        private System.Windows.Forms.TextBox peerTxt;
        private System.Windows.Forms.Button loadPeersBtn;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox consensusGrp;
        private System.Windows.Forms.Button validateBtn;
        private System.Windows.Forms.GroupBox chainExplorerGrpBox;
        private System.Windows.Forms.Button loadChainBtn;
        private System.Windows.Forms.DataGridView blocksGrid;
        private System.Windows.Forms.GroupBox blockMiningGrp;
        private System.Windows.Forms.TextBox nounceTxt;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button mineBtn;
        private System.Windows.Forms.GroupBox logsGrp;
        private System.Windows.Forms.GroupBox webServerCtlGrpBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox urlTxtBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.GroupBox webServerSettingsGrpBox;
        private System.Windows.Forms.Button saveSettingsBtn;
        private System.Windows.Forms.TextBox portTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox hostTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox minningSettingGrp;
        private System.Windows.Forms.NumericUpDown blockTimeSettingTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox nodeNameTxt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox autoMineGrp;
        private System.Windows.Forms.Button autoMineBtn;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown blockTimeTxt;
        private System.Windows.Forms.TabPage walletTab;
        private System.Windows.Forms.GroupBox queryBalanceGrp;
        private System.Windows.Forms.TextBox balanceTxt;
        private System.Windows.Forms.Button queryBalanceBtn;
        private System.Windows.Forms.GroupBox newTransGrpBox;
        private System.Windows.Forms.NumericUpDown amountTxt;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox receiverTxt;
        private System.Windows.Forms.Label receiverLbl;
        private System.Windows.Forms.TextBox senderTxt;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.RichTextBox logTxt;
        private System.Windows.Forms.TabPage memPoolTab;
        private System.Windows.Forms.GroupBox memPoolGrp;
        private System.Windows.Forms.Button loadMemPool;
        private System.Windows.Forms.DataGridView memPoolGrd;
    }
}

