namespace BlockChain.WinForms
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.webServerSettingsGrpBox = new System.Windows.Forms.GroupBox();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.portTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.webServerCtlGrpBox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.urlTxtBox = new System.Windows.Forms.TextBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.chainExplorerGrpBox = new System.Windows.Forms.GroupBox();
            this.loadChainBtn = new System.Windows.Forms.Button();
            this.blocksGrid = new System.Windows.Forms.DataGridView();
            this.transGrpBox = new System.Windows.Forms.GroupBox();
            this.transGrd = new System.Windows.Forms.DataGridView();
            this.newTransGrpBox = new System.Windows.Forms.GroupBox();
            this.amountTxt = new System.Windows.Forms.NumericUpDown();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.receiverTxt = new System.Windows.Forms.TextBox();
            this.receiverLbl = new System.Windows.Forms.Label();
            this.senderTxt = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.blockMiningGrp = new System.Windows.Forms.GroupBox();
            this.mineBtn = new System.Windows.Forms.Button();
            this.consensusGrp = new System.Windows.Forms.GroupBox();
            this.validateBtn = new System.Windows.Forms.Button();
            this.peersGrp = new System.Windows.Forms.GroupBox();
            this.addPeerBtn = new System.Windows.Forms.Button();
            this.peerTxt = new System.Windows.Forms.TextBox();
            this.peersLst = new System.Windows.Forms.ListBox();
            this.autoMineGrp = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.blockTimeTxt = new System.Windows.Forms.NumericUpDown();
            this.autoMineBtn = new System.Windows.Forms.Button();
            this.autoMineTimer = new System.Windows.Forms.Timer(this.components);
            this.logTxt = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.nounceTxt = new System.Windows.Forms.TextBox();
            this.webServerSettingsGrpBox.SuspendLayout();
            this.webServerCtlGrpBox.SuspendLayout();
            this.chainExplorerGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blocksGrid)).BeginInit();
            this.transGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transGrd)).BeginInit();
            this.newTransGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).BeginInit();
            this.blockMiningGrp.SuspendLayout();
            this.consensusGrp.SuspendLayout();
            this.peersGrp.SuspendLayout();
            this.autoMineGrp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockTimeTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // webServerSettingsGrpBox
            // 
            this.webServerSettingsGrpBox.Controls.Add(this.saveSettingsBtn);
            this.webServerSettingsGrpBox.Controls.Add(this.portTxtBox);
            this.webServerSettingsGrpBox.Controls.Add(this.label2);
            this.webServerSettingsGrpBox.Controls.Add(this.hostTxtBox);
            this.webServerSettingsGrpBox.Controls.Add(this.label1);
            this.webServerSettingsGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.webServerSettingsGrpBox.Location = new System.Drawing.Point(12, 12);
            this.webServerSettingsGrpBox.Name = "webServerSettingsGrpBox";
            this.webServerSettingsGrpBox.Size = new System.Drawing.Size(500, 100);
            this.webServerSettingsGrpBox.TabIndex = 0;
            this.webServerSettingsGrpBox.TabStop = false;
            this.webServerSettingsGrpBox.Text = "Node Listener Settings";
            // 
            // saveSettingsBtn
            // 
            this.saveSettingsBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.saveSettingsBtn.Location = new System.Drawing.Point(373, 39);
            this.saveSettingsBtn.Name = "saveSettingsBtn";
            this.saveSettingsBtn.Size = new System.Drawing.Size(75, 23);
            this.saveSettingsBtn.TabIndex = 4;
            this.saveSettingsBtn.Text = "Save";
            this.saveSettingsBtn.UseVisualStyleBackColor = true;
            this.saveSettingsBtn.Click += new System.EventHandler(this.saveSettingsBtn_Click);
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
            // webServerCtlGrpBox
            // 
            this.webServerCtlGrpBox.Controls.Add(this.label3);
            this.webServerCtlGrpBox.Controls.Add(this.urlTxtBox);
            this.webServerCtlGrpBox.Controls.Add(this.startBtn);
            this.webServerCtlGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.webServerCtlGrpBox.Location = new System.Drawing.Point(527, 12);
            this.webServerCtlGrpBox.Name = "webServerCtlGrpBox";
            this.webServerCtlGrpBox.Size = new System.Drawing.Size(485, 100);
            this.webServerCtlGrpBox.TabIndex = 1;
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
            this.urlTxtBox.Size = new System.Drawing.Size(266, 20);
            this.urlTxtBox.TabIndex = 1;
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.startBtn.Location = new System.Drawing.Point(372, 41);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(75, 23);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // chainExplorerGrpBox
            // 
            this.chainExplorerGrpBox.Controls.Add(this.loadChainBtn);
            this.chainExplorerGrpBox.Controls.Add(this.blocksGrid);
            this.chainExplorerGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.chainExplorerGrpBox.Location = new System.Drawing.Point(12, 118);
            this.chainExplorerGrpBox.Name = "chainExplorerGrpBox";
            this.chainExplorerGrpBox.Size = new System.Drawing.Size(997, 540);
            this.chainExplorerGrpBox.TabIndex = 2;
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
            this.blocksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.blocksGrid.DefaultCellStyle = dataGridViewCellStyle5;
            this.blocksGrid.Location = new System.Drawing.Point(6, 45);
            this.blocksGrid.Name = "blocksGrid";
            this.blocksGrid.ReadOnly = true;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Tahoma", 8F);
            dataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.blocksGrid.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.blocksGrid.Size = new System.Drawing.Size(985, 482);
            this.blocksGrid.TabIndex = 0;
            this.blocksGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.blocksGrid_CellMouseDoubleClick);
            // 
            // transGrpBox
            // 
            this.transGrpBox.Controls.Add(this.transGrd);
            this.transGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.transGrpBox.Location = new System.Drawing.Point(1270, 193);
            this.transGrpBox.Name = "transGrpBox";
            this.transGrpBox.Size = new System.Drawing.Size(495, 223);
            this.transGrpBox.TabIndex = 3;
            this.transGrpBox.TabStop = false;
            this.transGrpBox.Text = "Block Transactions";
            // 
            // transGrd
            // 
            this.transGrd.AllowUserToAddRows = false;
            this.transGrd.AllowUserToDeleteRows = false;
            this.transGrd.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.transGrd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transGrd.Location = new System.Drawing.Point(3, 16);
            this.transGrd.Name = "transGrd";
            this.transGrd.ReadOnly = true;
            this.transGrd.Size = new System.Drawing.Size(489, 204);
            this.transGrd.TabIndex = 0;
            // 
            // newTransGrpBox
            // 
            this.newTransGrpBox.Controls.Add(this.amountTxt);
            this.newTransGrpBox.Controls.Add(this.sendBtn);
            this.newTransGrpBox.Controls.Add(this.label5);
            this.newTransGrpBox.Controls.Add(this.receiverTxt);
            this.newTransGrpBox.Controls.Add(this.receiverLbl);
            this.newTransGrpBox.Controls.Add(this.senderTxt);
            this.newTransGrpBox.Controls.Add(this.label4);
            this.newTransGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.newTransGrpBox.Location = new System.Drawing.Point(1021, 193);
            this.newTransGrpBox.Name = "newTransGrpBox";
            this.newTransGrpBox.Size = new System.Drawing.Size(243, 220);
            this.newTransGrpBox.TabIndex = 4;
            this.newTransGrpBox.TabStop = false;
            this.newTransGrpBox.Text = "New Transaction";
            // 
            // amountTxt
            // 
            this.amountTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.amountTxt.Location = new System.Drawing.Point(58, 71);
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(167, 20);
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
            this.sendBtn.Location = new System.Drawing.Point(150, 97);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 7;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label5.Location = new System.Drawing.Point(9, 73);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Amount:";
            // 
            // receiverTxt
            // 
            this.receiverTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.receiverTxt.Location = new System.Drawing.Point(58, 45);
            this.receiverTxt.Name = "receiverTxt";
            this.receiverTxt.Size = new System.Drawing.Size(167, 20);
            this.receiverTxt.TabIndex = 3;
            this.receiverTxt.Text = "Ali";
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
            this.senderTxt.Size = new System.Drawing.Size(167, 20);
            this.senderTxt.TabIndex = 1;
            this.senderTxt.Text = "Alaa";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label4.Location = new System.Drawing.Point(12, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sender:";
            // 
            // blockMiningGrp
            // 
            this.blockMiningGrp.Controls.Add(this.nounceTxt);
            this.blockMiningGrp.Controls.Add(this.label7);
            this.blockMiningGrp.Controls.Add(this.mineBtn);
            this.blockMiningGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.blockMiningGrp.Location = new System.Drawing.Point(1030, 12);
            this.blockMiningGrp.Name = "blockMiningGrp";
            this.blockMiningGrp.Size = new System.Drawing.Size(160, 175);
            this.blockMiningGrp.TabIndex = 5;
            this.blockMiningGrp.TabStop = false;
            this.blockMiningGrp.Text = "Manual Block Mining";
            // 
            // mineBtn
            // 
            this.mineBtn.Enabled = false;
            this.mineBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mineBtn.Location = new System.Drawing.Point(21, 59);
            this.mineBtn.Name = "mineBtn";
            this.mineBtn.Size = new System.Drawing.Size(120, 50);
            this.mineBtn.TabIndex = 0;
            this.mineBtn.Text = "Mine";
            this.mineBtn.UseVisualStyleBackColor = true;
            this.mineBtn.Click += new System.EventHandler(this.mineBtn_Click);
            // 
            // consensusGrp
            // 
            this.consensusGrp.Controls.Add(this.validateBtn);
            this.consensusGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.consensusGrp.Location = new System.Drawing.Point(1600, 12);
            this.consensusGrp.Name = "consensusGrp";
            this.consensusGrp.Size = new System.Drawing.Size(161, 175);
            this.consensusGrp.TabIndex = 6;
            this.consensusGrp.TabStop = false;
            this.consensusGrp.Text = "Validate Consensus";
            // 
            // validateBtn
            // 
            this.validateBtn.Enabled = false;
            this.validateBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.validateBtn.Location = new System.Drawing.Point(23, 59);
            this.validateBtn.Name = "validateBtn";
            this.validateBtn.Size = new System.Drawing.Size(120, 50);
            this.validateBtn.TabIndex = 0;
            this.validateBtn.Text = "Validate";
            this.validateBtn.UseVisualStyleBackColor = true;
            this.validateBtn.Click += new System.EventHandler(this.validateBtn_Click);
            // 
            // peersGrp
            // 
            this.peersGrp.Controls.Add(this.addPeerBtn);
            this.peersGrp.Controls.Add(this.peerTxt);
            this.peersGrp.Controls.Add(this.peersLst);
            this.peersGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.peersGrp.Location = new System.Drawing.Point(1382, 12);
            this.peersGrp.Name = "peersGrp";
            this.peersGrp.Size = new System.Drawing.Size(212, 175);
            this.peersGrp.TabIndex = 7;
            this.peersGrp.TabStop = false;
            this.peersGrp.Text = "Nodes";
            // 
            // addPeerBtn
            // 
            this.addPeerBtn.Enabled = false;
            this.addPeerBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addPeerBtn.Location = new System.Drawing.Point(148, 33);
            this.addPeerBtn.Name = "addPeerBtn";
            this.addPeerBtn.Size = new System.Drawing.Size(41, 23);
            this.addPeerBtn.TabIndex = 2;
            this.addPeerBtn.Text = "+";
            this.addPeerBtn.UseVisualStyleBackColor = true;
            this.addPeerBtn.Click += new System.EventHandler(this.addPeerBtn_Click);
            // 
            // peerTxt
            // 
            this.peerTxt.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peerTxt.Location = new System.Drawing.Point(17, 33);
            this.peerTxt.Name = "peerTxt";
            this.peerTxt.Size = new System.Drawing.Size(124, 21);
            this.peerTxt.TabIndex = 1;
            // 
            // peersLst
            // 
            this.peersLst.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peersLst.FormattingEnabled = true;
            this.peersLst.Location = new System.Drawing.Point(17, 59);
            this.peersLst.Name = "peersLst";
            this.peersLst.Size = new System.Drawing.Size(172, 95);
            this.peersLst.TabIndex = 0;
            // 
            // autoMineGrp
            // 
            this.autoMineGrp.Controls.Add(this.autoMineBtn);
            this.autoMineGrp.Controls.Add(this.label6);
            this.autoMineGrp.Controls.Add(this.blockTimeTxt);
            this.autoMineGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.autoMineGrp.Location = new System.Drawing.Point(1197, 8);
            this.autoMineGrp.Name = "autoMineGrp";
            this.autoMineGrp.Size = new System.Drawing.Size(179, 179);
            this.autoMineGrp.TabIndex = 8;
            this.autoMineGrp.TabStop = false;
            this.autoMineGrp.Text = "Auto Block Mining";
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
            // autoMineBtn
            // 
            this.autoMineBtn.Enabled = false;
            this.autoMineBtn.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.autoMineBtn.Location = new System.Drawing.Point(31, 81);
            this.autoMineBtn.Name = "autoMineBtn";
            this.autoMineBtn.Size = new System.Drawing.Size(120, 50);
            this.autoMineBtn.TabIndex = 5;
            this.autoMineBtn.Text = "Start Auto-Mine";
            this.autoMineBtn.UseVisualStyleBackColor = true;
            this.autoMineBtn.Click += new System.EventHandler(this.autoMineBtn_Click);
            // 
            // autoMineTimer
            // 
            this.autoMineTimer.Tick += new System.EventHandler(this.autoMineTimer_Tick);
            // 
            // logTxt
            // 
            this.logTxt.Location = new System.Drawing.Point(1018, 422);
            this.logTxt.Multiline = true;
            this.logTxt.Name = "logTxt";
            this.logTxt.ReadOnly = true;
            this.logTxt.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.logTxt.Size = new System.Drawing.Size(747, 236);
            this.logTxt.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label7.Location = new System.Drawing.Point(31, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(87, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Current Nounce:";
            // 
            // nounceTxt
            // 
            this.nounceTxt.Location = new System.Drawing.Point(34, 138);
            this.nounceTxt.Name = "nounceTxt";
            this.nounceTxt.ReadOnly = true;
            this.nounceTxt.Size = new System.Drawing.Size(100, 20);
            this.nounceTxt.TabIndex = 7;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1777, 672);
            this.Controls.Add(this.consensusGrp);
            this.Controls.Add(this.autoMineGrp);
            this.Controls.Add(this.peersGrp);
            this.Controls.Add(this.blockMiningGrp);
            this.Controls.Add(this.newTransGrpBox);
            this.Controls.Add(this.transGrpBox);
            this.Controls.Add(this.chainExplorerGrpBox);
            this.Controls.Add(this.webServerCtlGrpBox);
            this.Controls.Add(this.webServerSettingsGrpBox);
            this.Controls.Add(this.logTxt);
            this.Name = "MainFrm";
            this.Text = "Blockchain Demo";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.webServerSettingsGrpBox.ResumeLayout(false);
            this.webServerSettingsGrpBox.PerformLayout();
            this.webServerCtlGrpBox.ResumeLayout(false);
            this.webServerCtlGrpBox.PerformLayout();
            this.chainExplorerGrpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.blocksGrid)).EndInit();
            this.transGrpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.transGrd)).EndInit();
            this.newTransGrpBox.ResumeLayout(false);
            this.newTransGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).EndInit();
            this.blockMiningGrp.ResumeLayout(false);
            this.blockMiningGrp.PerformLayout();
            this.consensusGrp.ResumeLayout(false);
            this.peersGrp.ResumeLayout(false);
            this.peersGrp.PerformLayout();
            this.autoMineGrp.ResumeLayout(false);
            this.autoMineGrp.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.blockTimeTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox webServerSettingsGrpBox;
        private System.Windows.Forms.TextBox hostTxtBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox portTxtBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button saveSettingsBtn;
        private System.Windows.Forms.GroupBox webServerCtlGrpBox;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.TextBox urlTxtBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox chainExplorerGrpBox;
        private System.Windows.Forms.Button loadChainBtn;
        private System.Windows.Forms.DataGridView blocksGrid;
        private System.Windows.Forms.GroupBox transGrpBox;
        private System.Windows.Forms.DataGridView transGrd;
        private System.Windows.Forms.GroupBox newTransGrpBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button sendBtn;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox receiverTxt;
        private System.Windows.Forms.Label receiverLbl;
        private System.Windows.Forms.TextBox senderTxt;
        private System.Windows.Forms.NumericUpDown amountTxt;
        private System.Windows.Forms.GroupBox blockMiningGrp;
        private System.Windows.Forms.Button mineBtn;
        private System.Windows.Forms.GroupBox consensusGrp;
        private System.Windows.Forms.Button validateBtn;
        private System.Windows.Forms.GroupBox peersGrp;
        private System.Windows.Forms.Button addPeerBtn;
        private System.Windows.Forms.TextBox peerTxt;
        private System.Windows.Forms.ListBox peersLst;
        private System.Windows.Forms.GroupBox autoMineGrp;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown blockTimeTxt;
        private System.Windows.Forms.Button autoMineBtn;
        private System.Windows.Forms.Timer autoMineTimer;
        private System.Windows.Forms.TextBox logTxt;
        private System.Windows.Forms.TextBox nounceTxt;
        private System.Windows.Forms.Label label7;
    }
}

