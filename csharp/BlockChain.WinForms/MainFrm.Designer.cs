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
            this.webServerSettingsGrpBox = new System.Windows.Forms.GroupBox();
            this.saveSettingsBtn = new System.Windows.Forms.Button();
            this.portTxtBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.hostTxtBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.webServerCtlGrpBox = new System.Windows.Forms.GroupBox();
            this.startBtn = new System.Windows.Forms.Button();
            this.urlTxtBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.chainExplorerGrpBox = new System.Windows.Forms.GroupBox();
            this.blocksGrid = new System.Windows.Forms.DataGridView();
            this.loadChainBtn = new System.Windows.Forms.Button();
            this.transGrpBox = new System.Windows.Forms.GroupBox();
            this.transGrd = new System.Windows.Forms.DataGridView();
            this.newTransGrpBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.senderTxt = new System.Windows.Forms.TextBox();
            this.receiverLbl = new System.Windows.Forms.Label();
            this.receiverTxt = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.sendBtn = new System.Windows.Forms.Button();
            this.amountTxt = new System.Windows.Forms.NumericUpDown();
            this.blockMiningGrp = new System.Windows.Forms.GroupBox();
            this.mineBtn = new System.Windows.Forms.Button();
            this.consensusGrp = new System.Windows.Forms.GroupBox();
            this.validateBtn = new System.Windows.Forms.Button();
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
            this.webServerSettingsGrpBox.Text = "Web Server Settings";
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
            this.webServerCtlGrpBox.Text = "Web Server Control";
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
            // urlTxtBox
            // 
            this.urlTxtBox.Location = new System.Drawing.Point(58, 43);
            this.urlTxtBox.Name = "urlTxtBox";
            this.urlTxtBox.ReadOnly = true;
            this.urlTxtBox.Size = new System.Drawing.Size(266, 20);
            this.urlTxtBox.TabIndex = 1;
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
            // chainExplorerGrpBox
            // 
            this.chainExplorerGrpBox.Controls.Add(this.loadChainBtn);
            this.chainExplorerGrpBox.Controls.Add(this.blocksGrid);
            this.chainExplorerGrpBox.Location = new System.Drawing.Point(12, 118);
            this.chainExplorerGrpBox.Name = "chainExplorerGrpBox";
            this.chainExplorerGrpBox.Size = new System.Drawing.Size(644, 342);
            this.chainExplorerGrpBox.TabIndex = 2;
            this.chainExplorerGrpBox.TabStop = false;
            this.chainExplorerGrpBox.Text = "Blockchain Explorer";
            // 
            // blocksGrid
            // 
            this.blocksGrid.AllowUserToAddRows = false;
            this.blocksGrid.AllowUserToDeleteRows = false;
            this.blocksGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.blocksGrid.Location = new System.Drawing.Point(6, 45);
            this.blocksGrid.Name = "blocksGrid";
            this.blocksGrid.ReadOnly = true;
            this.blocksGrid.Size = new System.Drawing.Size(620, 291);
            this.blocksGrid.TabIndex = 0;
            this.blocksGrid.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.blocksGrid_CellMouseDoubleClick);
            // 
            // loadChainBtn
            // 
            this.loadChainBtn.Enabled = false;
            this.loadChainBtn.Location = new System.Drawing.Point(6, 16);
            this.loadChainBtn.Name = "loadChainBtn";
            this.loadChainBtn.Size = new System.Drawing.Size(75, 23);
            this.loadChainBtn.TabIndex = 1;
            this.loadChainBtn.Text = "Load";
            this.loadChainBtn.UseVisualStyleBackColor = true;
            this.loadChainBtn.Click += new System.EventHandler(this.loadChainBtn_Click);
            // 
            // transGrpBox
            // 
            this.transGrpBox.Controls.Add(this.transGrd);
            this.transGrpBox.Location = new System.Drawing.Point(666, 118);
            this.transGrpBox.Name = "transGrpBox";
            this.transGrpBox.Size = new System.Drawing.Size(346, 342);
            this.transGrpBox.TabIndex = 3;
            this.transGrpBox.TabStop = false;
            this.transGrpBox.Text = "Transactions";
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
            this.transGrd.Size = new System.Drawing.Size(340, 323);
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
            this.newTransGrpBox.Location = new System.Drawing.Point(18, 467);
            this.newTransGrpBox.Name = "newTransGrpBox";
            this.newTransGrpBox.Size = new System.Drawing.Size(597, 100);
            this.newTransGrpBox.TabIndex = 4;
            this.newTransGrpBox.TabStop = false;
            this.newTransGrpBox.Text = "New Transaction";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label4.Location = new System.Drawing.Point(7, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Sender:";
            // 
            // senderTxt
            // 
            this.senderTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.senderTxt.Location = new System.Drawing.Point(58, 43);
            this.senderTxt.Name = "senderTxt";
            this.senderTxt.Size = new System.Drawing.Size(100, 20);
            this.senderTxt.TabIndex = 1;
            this.senderTxt.Text = "Alaa";
            // 
            // receiverLbl
            // 
            this.receiverLbl.AutoSize = true;
            this.receiverLbl.Font = new System.Drawing.Font("Tahoma", 8F);
            this.receiverLbl.Location = new System.Drawing.Point(164, 43);
            this.receiverLbl.Name = "receiverLbl";
            this.receiverLbl.Size = new System.Drawing.Size(53, 13);
            this.receiverLbl.TabIndex = 2;
            this.receiverLbl.Text = "Receiver:";
            // 
            // receiverTxt
            // 
            this.receiverTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.receiverTxt.Location = new System.Drawing.Point(223, 43);
            this.receiverTxt.Name = "receiverTxt";
            this.receiverTxt.Size = new System.Drawing.Size(100, 20);
            this.receiverTxt.TabIndex = 3;
            this.receiverTxt.Text = "Ali";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Tahoma", 8F);
            this.label5.Location = new System.Drawing.Point(329, 43);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Amount:";
            // 
            // sendBtn
            // 
            this.sendBtn.Enabled = false;
            this.sendBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.sendBtn.Location = new System.Drawing.Point(509, 38);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 23);
            this.sendBtn.TabIndex = 7;
            this.sendBtn.Text = "Send";
            this.sendBtn.UseVisualStyleBackColor = true;
            this.sendBtn.Click += new System.EventHandler(this.sendBtn_Click);
            // 
            // amountTxt
            // 
            this.amountTxt.Font = new System.Drawing.Font("Tahoma", 8F);
            this.amountTxt.Location = new System.Drawing.Point(383, 41);
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(120, 20);
            this.amountTxt.TabIndex = 6;
            this.amountTxt.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            // 
            // blockMiningGrp
            // 
            this.blockMiningGrp.Controls.Add(this.mineBtn);
            this.blockMiningGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.blockMiningGrp.Location = new System.Drawing.Point(622, 467);
            this.blockMiningGrp.Name = "blockMiningGrp";
            this.blockMiningGrp.Size = new System.Drawing.Size(190, 100);
            this.blockMiningGrp.TabIndex = 5;
            this.blockMiningGrp.TabStop = false;
            this.blockMiningGrp.Text = "Block Mining";
            // 
            // mineBtn
            // 
            this.mineBtn.Enabled = false;
            this.mineBtn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.mineBtn.Location = new System.Drawing.Point(22, 25);
            this.mineBtn.Name = "mineBtn";
            this.mineBtn.Size = new System.Drawing.Size(150, 50);
            this.mineBtn.TabIndex = 0;
            this.mineBtn.Text = "Mine";
            this.mineBtn.UseVisualStyleBackColor = true;
            this.mineBtn.Click += new System.EventHandler(this.mineBtn_Click);
            // 
            // consensusGrp
            // 
            this.consensusGrp.Controls.Add(this.validateBtn);
            this.consensusGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.consensusGrp.Location = new System.Drawing.Point(819, 467);
            this.consensusGrp.Name = "consensusGrp";
            this.consensusGrp.Size = new System.Drawing.Size(190, 100);
            this.consensusGrp.TabIndex = 6;
            this.consensusGrp.TabStop = false;
            this.consensusGrp.Text = "Validate Consensus";
            // 
            // validateBtn
            // 
            this.validateBtn.Enabled = false;
            this.validateBtn.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.validateBtn.Location = new System.Drawing.Point(22, 25);
            this.validateBtn.Name = "validateBtn";
            this.validateBtn.Size = new System.Drawing.Size(150, 50);
            this.validateBtn.TabIndex = 0;
            this.validateBtn.Text = "Validate";
            this.validateBtn.UseVisualStyleBackColor = true;
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1039, 745);
            this.Controls.Add(this.consensusGrp);
            this.Controls.Add(this.blockMiningGrp);
            this.Controls.Add(this.newTransGrpBox);
            this.Controls.Add(this.transGrpBox);
            this.Controls.Add(this.chainExplorerGrpBox);
            this.Controls.Add(this.webServerCtlGrpBox);
            this.Controls.Add(this.webServerSettingsGrpBox);
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
            this.consensusGrp.ResumeLayout(false);
            this.ResumeLayout(false);

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
    }
}

