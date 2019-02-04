namespace BlockChain.Miner
{
    partial class ShowTransactionsDialog
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
            this.transGrpBox = new System.Windows.Forms.GroupBox();
            this.transGrd = new System.Windows.Forms.DataGridView();
            this.transGrpBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transGrd)).BeginInit();
            this.SuspendLayout();
            // 
            // transGrpBox
            // 
            this.transGrpBox.Controls.Add(this.transGrd);
            this.transGrpBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.transGrpBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.transGrpBox.Location = new System.Drawing.Point(0, 0);
            this.transGrpBox.Name = "transGrpBox";
            this.transGrpBox.Size = new System.Drawing.Size(750, 374);
            this.transGrpBox.TabIndex = 4;
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
            this.transGrd.Size = new System.Drawing.Size(744, 355);
            this.transGrd.TabIndex = 0;
            // 
            // ShowTransactionsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 374);
            this.Controls.Add(this.transGrpBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "ShowTransactionsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Show Transactions";
            this.Load += new System.EventHandler(this.ShowTransactionsDialog_Load);
            this.transGrpBox.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.transGrd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox transGrpBox;
        private System.Windows.Forms.DataGridView transGrd;
    }
}