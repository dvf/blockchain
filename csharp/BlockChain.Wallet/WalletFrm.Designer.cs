namespace BlockChain.Wallet
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
            this.queryBalanceGrp = new System.Windows.Forms.GroupBox();
            this.balanceTxt = new System.Windows.Forms.TextBox();
            this.queryBalanceBtn = new System.Windows.Forms.Button();
            this.newTransGrpBox = new System.Windows.Forms.GroupBox();
            this.amountTxt = new System.Windows.Forms.NumericUpDown();
            this.sendBtn = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.receiverTxt = new System.Windows.Forms.TextBox();
            this.receiverLbl = new System.Windows.Forms.Label();
            this.senderTxt = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.nodeGrp = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nodeCmb = new System.Windows.Forms.ComboBox();
            this.connectBtn = new System.Windows.Forms.Button();
            this.queryBalanceGrp.SuspendLayout();
            this.newTransGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).BeginInit();
            this.nodeGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // queryBalanceGrp
            // 
            this.queryBalanceGrp.Controls.Add(this.balanceTxt);
            this.queryBalanceGrp.Controls.Add(this.queryBalanceBtn);
            this.queryBalanceGrp.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.queryBalanceGrp.Location = new System.Drawing.Point(12, 106);
            this.queryBalanceGrp.Name = "queryBalanceGrp";
            this.queryBalanceGrp.Size = new System.Drawing.Size(169, 141);
            this.queryBalanceGrp.TabIndex = 17;
            this.queryBalanceGrp.TabStop = false;
            this.queryBalanceGrp.Text = "Query Balance";
            // 
            // balanceTxt
            // 
            this.balanceTxt.Location = new System.Drawing.Point(32, 33);
            this.balanceTxt.Name = "balanceTxt";
            this.balanceTxt.ReadOnly = true;
            this.balanceTxt.Size = new System.Drawing.Size(94, 20);
            this.balanceTxt.TabIndex = 8;
            // 
            // queryBalanceBtn
            // 
            this.queryBalanceBtn.Enabled = false;
            this.queryBalanceBtn.Font = new System.Drawing.Font("Tahoma", 8F);
            this.queryBalanceBtn.Location = new System.Drawing.Point(32, 73);
            this.queryBalanceBtn.Name = "queryBalanceBtn";
            this.queryBalanceBtn.Size = new System.Drawing.Size(94, 42);
            this.queryBalanceBtn.TabIndex = 0;
            this.queryBalanceBtn.Text = "Query Balance";
            this.queryBalanceBtn.UseVisualStyleBackColor = true;
            this.queryBalanceBtn.Click += new System.EventHandler(this.queryBalanceBtn_Click);
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
            this.newTransGrpBox.Location = new System.Drawing.Point(187, 106);
            this.newTransGrpBox.Name = "newTransGrpBox";
            this.newTransGrpBox.Size = new System.Drawing.Size(397, 141);
            this.newTransGrpBox.TabIndex = 18;
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
            this.amountTxt.Size = new System.Drawing.Size(244, 20);
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
            this.sendBtn.Location = new System.Drawing.Point(308, 18);
            this.sendBtn.Name = "sendBtn";
            this.sendBtn.Size = new System.Drawing.Size(75, 72);
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
            this.receiverTxt.Size = new System.Drawing.Size(244, 20);
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
            this.senderTxt.Size = new System.Drawing.Size(244, 20);
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
            // nodeGrp
            // 
            this.nodeGrp.Controls.Add(this.connectBtn);
            this.nodeGrp.Controls.Add(this.label1);
            this.nodeGrp.Controls.Add(this.nodeCmb);
            this.nodeGrp.Location = new System.Drawing.Point(13, 13);
            this.nodeGrp.Name = "nodeGrp";
            this.nodeGrp.Size = new System.Drawing.Size(571, 87);
            this.nodeGrp.TabIndex = 19;
            this.nodeGrp.TabStop = false;
            this.nodeGrp.Text = "BlockChain Node";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Select a Blockchain Node:";
            // 
            // nodeCmb
            // 
            this.nodeCmb.FormattingEnabled = true;
            this.nodeCmb.Items.AddRange(new object[] {
            "http://localhost:8888/",
            "http://localhost:9999/",
            "http://localhost:7777/"});
            this.nodeCmb.Location = new System.Drawing.Point(174, 29);
            this.nodeCmb.Name = "nodeCmb";
            this.nodeCmb.Size = new System.Drawing.Size(302, 21);
            this.nodeCmb.TabIndex = 0;
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(482, 29);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(75, 23);
            this.connectBtn.TabIndex = 2;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 258);
            this.Controls.Add(this.nodeGrp);
            this.Controls.Add(this.newTransGrpBox);
            this.Controls.Add(this.queryBalanceGrp);
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Wallet";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.queryBalanceGrp.ResumeLayout(false);
            this.queryBalanceGrp.PerformLayout();
            this.newTransGrpBox.ResumeLayout(false);
            this.newTransGrpBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).EndInit();
            this.nodeGrp.ResumeLayout(false);
            this.nodeGrp.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

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
        private System.Windows.Forms.GroupBox nodeGrp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox nodeCmb;
        private System.Windows.Forms.Button connectBtn;
    }
}

