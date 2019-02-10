namespace BlockChain.Wallet
{
    partial class UnlockFrm
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
            this.label1 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.privateKeyTxt = new System.Windows.Forms.TextBox();
            this.openBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.walletAddressTxt = new System.Windows.Forms.TextBox();
            this.createWalletBtn = new System.Windows.Forms.Button();
            this.loginGrp = new System.Windows.Forms.GroupBox();
            this.loginBtn = new System.Windows.Forms.Button();
            this.createWalletGrp = new System.Windows.Forms.GroupBox();
            this.loginGrp.SuspendLayout();
            this.createWalletGrp.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Select Key Pair";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // privateKeyTxt
            // 
            this.privateKeyTxt.Location = new System.Drawing.Point(20, 45);
            this.privateKeyTxt.Name = "privateKeyTxt";
            this.privateKeyTxt.ReadOnly = true;
            this.privateKeyTxt.Size = new System.Drawing.Size(269, 20);
            this.privateKeyTxt.TabIndex = 1;
            // 
            // openBtn
            // 
            this.openBtn.Location = new System.Drawing.Point(296, 45);
            this.openBtn.Name = "openBtn";
            this.openBtn.Size = new System.Drawing.Size(43, 20);
            this.openBtn.TabIndex = 2;
            this.openBtn.Text = "..";
            this.openBtn.UseVisualStyleBackColor = true;
            this.openBtn.Click += new System.EventHandler(this.openBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Your wallet address";
            // 
            // walletAddressTxt
            // 
            this.walletAddressTxt.Location = new System.Drawing.Point(20, 96);
            this.walletAddressTxt.Name = "walletAddressTxt";
            this.walletAddressTxt.ReadOnly = true;
            this.walletAddressTxt.Size = new System.Drawing.Size(269, 20);
            this.walletAddressTxt.TabIndex = 4;
            // 
            // createWalletBtn
            // 
            this.createWalletBtn.Location = new System.Drawing.Point(118, 31);
            this.createWalletBtn.Name = "createWalletBtn";
            this.createWalletBtn.Size = new System.Drawing.Size(93, 42);
            this.createWalletBtn.TabIndex = 5;
            this.createWalletBtn.Text = "Create Wallet";
            this.createWalletBtn.UseVisualStyleBackColor = true;
            this.createWalletBtn.Click += new System.EventHandler(this.createWalletBtn_Click);
            // 
            // loginGrp
            // 
            this.loginGrp.Controls.Add(this.loginBtn);
            this.loginGrp.Controls.Add(this.privateKeyTxt);
            this.loginGrp.Controls.Add(this.label1);
            this.loginGrp.Controls.Add(this.walletAddressTxt);
            this.loginGrp.Controls.Add(this.openBtn);
            this.loginGrp.Controls.Add(this.label2);
            this.loginGrp.Location = new System.Drawing.Point(31, 12);
            this.loginGrp.Name = "loginGrp";
            this.loginGrp.Size = new System.Drawing.Size(356, 164);
            this.loginGrp.TabIndex = 6;
            this.loginGrp.TabStop = false;
            this.loginGrp.Text = "Login";
            // 
            // loginBtn
            // 
            this.loginBtn.Location = new System.Drawing.Point(136, 135);
            this.loginBtn.Name = "loginBtn";
            this.loginBtn.Size = new System.Drawing.Size(75, 23);
            this.loginBtn.TabIndex = 5;
            this.loginBtn.Text = "Login";
            this.loginBtn.UseVisualStyleBackColor = true;
            this.loginBtn.Click += new System.EventHandler(this.loginBtn_Click);
            // 
            // createWalletGrp
            // 
            this.createWalletGrp.Controls.Add(this.createWalletBtn);
            this.createWalletGrp.Location = new System.Drawing.Point(31, 182);
            this.createWalletGrp.Name = "createWalletGrp";
            this.createWalletGrp.Size = new System.Drawing.Size(356, 100);
            this.createWalletGrp.TabIndex = 7;
            this.createWalletGrp.TabStop = false;
            this.createWalletGrp.Text = "Create Wallet";
            // 
            // UnlockFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 291);
            this.Controls.Add(this.createWalletGrp);
            this.Controls.Add(this.loginGrp);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "UnlockFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login";
            this.loginGrp.ResumeLayout(false);
            this.loginGrp.PerformLayout();
            this.createWalletGrp.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox privateKeyTxt;
        private System.Windows.Forms.Button openBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox walletAddressTxt;
        private System.Windows.Forms.Button createWalletBtn;
        private System.Windows.Forms.GroupBox loginGrp;
        private System.Windows.Forms.Button loginBtn;
        private System.Windows.Forms.GroupBox createWalletGrp;
    }
}