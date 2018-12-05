namespace UCOSPasswordDecrypter
{
    partial class frmSelectUCOSHost
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
            this.tbUCOSHost = new System.Windows.Forms.TextBox();
            this.tbRemoteUser = new System.Windows.Forms.TextBox();
            this.tbRemotePassphrase = new System.Windows.Forms.TextBox();
            this.lblUCOSHost = new System.Windows.Forms.Label();
            this.lblRemoteUser = new System.Windows.Forms.Label();
            this.lblRemotePassphrase = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tbUCOSHost
            // 
            this.tbUCOSHost.Location = new System.Drawing.Point(111, 12);
            this.tbUCOSHost.Name = "tbUCOSHost";
            this.tbUCOSHost.Size = new System.Drawing.Size(193, 20);
            this.tbUCOSHost.TabIndex = 0;
            // 
            // tbRemoteUser
            // 
            this.tbRemoteUser.Location = new System.Drawing.Point(111, 38);
            this.tbRemoteUser.Name = "tbRemoteUser";
            this.tbRemoteUser.Size = new System.Drawing.Size(193, 20);
            this.tbRemoteUser.TabIndex = 1;
            // 
            // tbRemotePassphrase
            // 
            this.tbRemotePassphrase.Location = new System.Drawing.Point(111, 64);
            this.tbRemotePassphrase.Name = "tbRemotePassphrase";
            this.tbRemotePassphrase.Size = new System.Drawing.Size(193, 20);
            this.tbRemotePassphrase.TabIndex = 2;
            this.tbRemotePassphrase.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // lblUCOSHost
            // 
            this.lblUCOSHost.AutoSize = true;
            this.lblUCOSHost.Location = new System.Drawing.Point(2, 15);
            this.lblUCOSHost.Name = "lblUCOSHost";
            this.lblUCOSHost.Size = new System.Drawing.Size(62, 13);
            this.lblUCOSHost.TabIndex = 3;
            this.lblUCOSHost.Text = "UCOS Host";
            this.lblUCOSHost.Click += new System.EventHandler(this.lblUCOSHost_Click);
            // 
            // lblRemoteUser
            // 
            this.lblRemoteUser.AutoSize = true;
            this.lblRemoteUser.Location = new System.Drawing.Point(2, 41);
            this.lblRemoteUser.Name = "lblRemoteUser";
            this.lblRemoteUser.Size = new System.Drawing.Size(69, 13);
            this.lblRemoteUser.TabIndex = 4;
            this.lblRemoteUser.Text = "Remote User";
            // 
            // lblRemotePassphrase
            // 
            this.lblRemotePassphrase.AutoSize = true;
            this.lblRemotePassphrase.Location = new System.Drawing.Point(2, 67);
            this.lblRemotePassphrase.Name = "lblRemotePassphrase";
            this.lblRemotePassphrase.Size = new System.Drawing.Size(102, 13);
            this.lblRemotePassphrase.TabIndex = 5;
            this.lblRemotePassphrase.Text = "Remote Passphrase";
            this.lblRemotePassphrase.Click += new System.EventHandler(this.lblRemotePassphrase_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(152, 90);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // frmSelectUCOSHost
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(318, 123);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.lblRemotePassphrase);
            this.Controls.Add(this.lblRemoteUser);
            this.Controls.Add(this.lblUCOSHost);
            this.Controls.Add(this.tbRemotePassphrase);
            this.Controls.Add(this.tbRemoteUser);
            this.Controls.Add(this.tbUCOSHost);
            this.Name = "frmSelectUCOSHost";
            this.Text = "Connect to UCOS Host";
            this.Load += new System.EventHandler(this.frmSelectUCOSHost_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbUCOSHost;
        private System.Windows.Forms.TextBox tbRemoteUser;
        private System.Windows.Forms.TextBox tbRemotePassphrase;
        private System.Windows.Forms.Label lblUCOSHost;
        private System.Windows.Forms.Label lblRemoteUser;
        private System.Windows.Forms.Label lblRemotePassphrase;
        private System.Windows.Forms.Button btnConnect;
    }
}