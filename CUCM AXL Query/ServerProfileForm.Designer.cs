namespace CUCM_AXL_Query
{
    partial class ServerProfileForm
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
            this.lbl_svrForm_CUCMHost = new System.Windows.Forms.Label();
            this.lbl_svrForm_AXLUser = new System.Windows.Forms.Label();
            this.lbl_svrForm_AXLPass = new System.Windows.Forms.Label();
            this.tb_AXLHost = new System.Windows.Forms.TextBox();
            this.tb_AXLUser = new System.Windows.Forms.TextBox();
            this.tb_AXLPass = new System.Windows.Forms.TextBox();
            this.btn_AXLProfileValidate = new System.Windows.Forms.Button();
            this.btn_AXLProfileSubmit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_svrForm_CUCMHost
            // 
            this.lbl_svrForm_CUCMHost.AutoSize = true;
            this.lbl_svrForm_CUCMHost.Location = new System.Drawing.Point(12, 22);
            this.lbl_svrForm_CUCMHost.Name = "lbl_svrForm_CUCMHost";
            this.lbl_svrForm_CUCMHost.Size = new System.Drawing.Size(63, 13);
            this.lbl_svrForm_CUCMHost.TabIndex = 0;
            this.lbl_svrForm_CUCMHost.Text = "CUCM Host";
            // 
            // lbl_svrForm_AXLUser
            // 
            this.lbl_svrForm_AXLUser.AutoSize = true;
            this.lbl_svrForm_AXLUser.Location = new System.Drawing.Point(12, 50);
            this.lbl_svrForm_AXLUser.Name = "lbl_svrForm_AXLUser";
            this.lbl_svrForm_AXLUser.Size = new System.Drawing.Size(52, 13);
            this.lbl_svrForm_AXLUser.TabIndex = 1;
            this.lbl_svrForm_AXLUser.Text = "AXL User";
            // 
            // lbl_svrForm_AXLPass
            // 
            this.lbl_svrForm_AXLPass.AutoSize = true;
            this.lbl_svrForm_AXLPass.Location = new System.Drawing.Point(12, 81);
            this.lbl_svrForm_AXLPass.Name = "lbl_svrForm_AXLPass";
            this.lbl_svrForm_AXLPass.Size = new System.Drawing.Size(76, 13);
            this.lbl_svrForm_AXLPass.TabIndex = 2;
            this.lbl_svrForm_AXLPass.Text = "AXL Password";
            // 
            // tb_AXLHost
            // 
            this.tb_AXLHost.Location = new System.Drawing.Point(106, 19);
            this.tb_AXLHost.Name = "tb_AXLHost";
            this.tb_AXLHost.Size = new System.Drawing.Size(202, 20);
            this.tb_AXLHost.TabIndex = 3;
            this.tb_AXLHost.TextChanged += new System.EventHandler(this.tb_AXLHost_TextChanged);
            // 
            // tb_AXLUser
            // 
            this.tb_AXLUser.Location = new System.Drawing.Point(106, 47);
            this.tb_AXLUser.Name = "tb_AXLUser";
            this.tb_AXLUser.Size = new System.Drawing.Size(202, 20);
            this.tb_AXLUser.TabIndex = 4;
            // 
            // tb_AXLPass
            // 
            this.tb_AXLPass.Location = new System.Drawing.Point(106, 78);
            this.tb_AXLPass.Name = "tb_AXLPass";
            this.tb_AXLPass.PasswordChar = '*';
            this.tb_AXLPass.Size = new System.Drawing.Size(202, 20);
            this.tb_AXLPass.TabIndex = 5;
            // 
            // btn_AXLProfileValidate
            // 
            this.btn_AXLProfileValidate.Location = new System.Drawing.Point(67, 110);
            this.btn_AXLProfileValidate.Name = "btn_AXLProfileValidate";
            this.btn_AXLProfileValidate.Size = new System.Drawing.Size(75, 23);
            this.btn_AXLProfileValidate.TabIndex = 6;
            this.btn_AXLProfileValidate.Text = "Validate";
            this.btn_AXLProfileValidate.UseVisualStyleBackColor = true;
            this.btn_AXLProfileValidate.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_AXLProfileSubmit
            // 
            this.btn_AXLProfileSubmit.Location = new System.Drawing.Point(179, 110);
            this.btn_AXLProfileSubmit.Name = "btn_AXLProfileSubmit";
            this.btn_AXLProfileSubmit.Size = new System.Drawing.Size(75, 23);
            this.btn_AXLProfileSubmit.TabIndex = 7;
            this.btn_AXLProfileSubmit.Text = "Submit";
            this.btn_AXLProfileSubmit.UseVisualStyleBackColor = true;
            this.btn_AXLProfileSubmit.Click += new System.EventHandler(this.btn_AXLProfileSubmit_Click);
            // 
            // ServerProfileForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 145);
            this.Controls.Add(this.btn_AXLProfileSubmit);
            this.Controls.Add(this.btn_AXLProfileValidate);
            this.Controls.Add(this.tb_AXLPass);
            this.Controls.Add(this.tb_AXLUser);
            this.Controls.Add(this.tb_AXLHost);
            this.Controls.Add(this.lbl_svrForm_AXLPass);
            this.Controls.Add(this.lbl_svrForm_AXLUser);
            this.Controls.Add(this.lbl_svrForm_CUCMHost);
            this.Name = "ServerProfileForm";
            this.Text = "Server Profile";
            this.Load += new System.EventHandler(this.ServerProfileForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_svrForm_CUCMHost;
        private System.Windows.Forms.Label lbl_svrForm_AXLUser;
        private System.Windows.Forms.Label lbl_svrForm_AXLPass;
        private System.Windows.Forms.TextBox tb_AXLHost;
        private System.Windows.Forms.TextBox tb_AXLUser;
        private System.Windows.Forms.TextBox tb_AXLPass;
        private System.Windows.Forms.Button btn_AXLProfileValidate;
        private System.Windows.Forms.Button btn_AXLProfileSubmit;
    }
}