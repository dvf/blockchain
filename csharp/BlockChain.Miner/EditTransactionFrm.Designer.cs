namespace BlockChain.Miner
{
    partial class EditTransactionFrm
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
            this.senderTXT = new System.Windows.Forms.TextBox();
            this.receiverTxt = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.amountTxt = new System.Windows.Forms.NumericUpDown();
            this.editBtn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Sender :";
            // 
            // senderTXT
            // 
            this.senderTXT.Location = new System.Drawing.Point(80, 12);
            this.senderTXT.Name = "senderTXT";
            this.senderTXT.Size = new System.Drawing.Size(387, 20);
            this.senderTXT.TabIndex = 1;
            // 
            // receiverTxt
            // 
            this.receiverTxt.Location = new System.Drawing.Point(80, 39);
            this.receiverTxt.Name = "receiverTxt";
            this.receiverTxt.Size = new System.Drawing.Size(387, 20);
            this.receiverTxt.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 39);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Receipent :";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Amount :";
            // 
            // amountTxt
            // 
            this.amountTxt.Location = new System.Drawing.Point(79, 65);
            this.amountTxt.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.amountTxt.Name = "amountTxt";
            this.amountTxt.Size = new System.Drawing.Size(388, 20);
            this.amountTxt.TabIndex = 5;
            // 
            // editBtn
            // 
            this.editBtn.Location = new System.Drawing.Point(392, 95);
            this.editBtn.Name = "editBtn";
            this.editBtn.Size = new System.Drawing.Size(75, 23);
            this.editBtn.TabIndex = 6;
            this.editBtn.Text = "Edit";
            this.editBtn.UseVisualStyleBackColor = true;
            this.editBtn.Click += new System.EventHandler(this.editBtn_Click);
            // 
            // EditTransactionFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(479, 130);
            this.Controls.Add(this.editBtn);
            this.Controls.Add(this.amountTxt);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.receiverTxt);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.senderTXT);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "EditTransactionFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Transaction";
            this.Load += new System.EventHandler(this.EditTransactionFrm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.amountTxt)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox senderTXT;
        private System.Windows.Forms.TextBox receiverTxt;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown amountTxt;
        private System.Windows.Forms.Button editBtn;
    }
}