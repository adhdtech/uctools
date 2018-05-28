namespace ADHDTech.CiscoCrypt
{
    partial class DecodePassphraseForm
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
            this.tbRemoteSupportPassphrase = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbDecodedPassword = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbRemoteSupportPassphrase
            // 
            this.tbRemoteSupportPassphrase.Location = new System.Drawing.Point(159, 12);
            this.tbRemoteSupportPassphrase.Name = "tbRemoteSupportPassphrase";
            this.tbRemoteSupportPassphrase.Size = new System.Drawing.Size(201, 20);
            this.tbRemoteSupportPassphrase.TabIndex = 0;
            this.tbRemoteSupportPassphrase.TextChanged += new System.EventHandler(this.tbRemoteSupportPassphrase_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Remote Support Passphrase";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(100, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Decoded Password";
            // 
            // tbDecodedPassword
            // 
            this.tbDecodedPassword.Location = new System.Drawing.Point(159, 39);
            this.tbDecodedPassword.Name = "tbDecodedPassword";
            this.tbDecodedPassword.ReadOnly = true;
            this.tbDecodedPassword.Size = new System.Drawing.Size(201, 20);
            this.tbDecodedPassword.TabIndex = 3;
            // 
            // DecodePassphraseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 78);
            this.Controls.Add(this.tbDecodedPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbRemoteSupportPassphrase);
            this.Name = "DecodePassphraseForm";
            this.Text = "Decode Passphrase";
            this.Load += new System.EventHandler(this.DecodePassphraseForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbRemoteSupportPassphrase;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbDecodedPassword;
    }
}