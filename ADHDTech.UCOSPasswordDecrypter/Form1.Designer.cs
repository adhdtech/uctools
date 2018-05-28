namespace WindowsFormsApplication1
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.textBox_UCVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_UCProduct = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_LocalAdminName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.textBox_LocalAdminPass = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox_SFTPPass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_AppUserPass = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox_AppUserName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.textBox_ClusterSecurity = new System.Windows.Forms.TextBox();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(572, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            this.menuStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.menuStrip1_ItemClicked);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem1.Image")));
            this.toolStripMenuItem1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItem1.Size = new System.Drawing.Size(255, 22);
            this.toolStripMenuItem1.Text = "&Open Platform Config File";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(252, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(255, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator5,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            this.helpToolStripMenuItem.Click += new System.EventHandler(this.helpToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.aboutToolStripMenuItem.Text = "&About...";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "Platform Config XML|*platformConfig*.xml";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 36);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(97, 13);
            this.label6.TabIndex = 12;
            this.label6.Text = "Platform Config File";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // textBox3
            // 
            this.textBox3.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.Location = new System.Drawing.Point(117, 35);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(350, 18);
            this.textBox3.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(482, 35);
            this.button2.Name = "button2";
            this.button2.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.button2.Size = new System.Drawing.Size(64, 21);
            this.button2.TabIndex = 10;
            this.button2.Text = "Select File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBox_UCVersion
            // 
            this.textBox_UCVersion.AllowDrop = true;
            this.textBox_UCVersion.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_UCVersion.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_UCVersion.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_UCVersion.Location = new System.Drawing.Point(117, 64);
            this.textBox_UCVersion.Name = "textBox_UCVersion";
            this.textBox_UCVersion.ReadOnly = true;
            this.textBox_UCVersion.Size = new System.Drawing.Size(150, 11);
            this.textBox_UCVersion.TabIndex = 2;
            this.textBox_UCVersion.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "UC Version";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "UC Product";
            this.label2.Click += new System.EventHandler(this.label2_Click_3);
            // 
            // textBox_UCProduct
            // 
            this.textBox_UCProduct.AllowDrop = true;
            this.textBox_UCProduct.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_UCProduct.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_UCProduct.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_UCProduct.Location = new System.Drawing.Point(117, 88);
            this.textBox_UCProduct.Name = "textBox_UCProduct";
            this.textBox_UCProduct.ReadOnly = true;
            this.textBox_UCProduct.Size = new System.Drawing.Size(150, 11);
            this.textBox_UCProduct.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 13);
            this.label3.TabIndex = 19;
            this.label3.Text = "Local Admin Name";
            this.label3.Click += new System.EventHandler(this.label3_Click_1);
            // 
            // textBox_LocalAdminName
            // 
            this.textBox_LocalAdminName.AllowDrop = true;
            this.textBox_LocalAdminName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_LocalAdminName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LocalAdminName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_LocalAdminName.Location = new System.Drawing.Point(117, 112);
            this.textBox_LocalAdminName.Name = "textBox_LocalAdminName";
            this.textBox_LocalAdminName.ReadOnly = true;
            this.textBox_LocalAdminName.Size = new System.Drawing.Size(150, 11);
            this.textBox_LocalAdminName.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 135);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 21;
            this.label4.Text = "Local Admin Pass";
            // 
            // textBox_LocalAdminPass
            // 
            this.textBox_LocalAdminPass.AllowDrop = true;
            this.textBox_LocalAdminPass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_LocalAdminPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_LocalAdminPass.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_LocalAdminPass.Location = new System.Drawing.Point(117, 136);
            this.textBox_LocalAdminPass.Name = "textBox_LocalAdminPass";
            this.textBox_LocalAdminPass.ReadOnly = true;
            this.textBox_LocalAdminPass.Size = new System.Drawing.Size(150, 11);
            this.textBox_LocalAdminPass.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(284, 87);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(60, 13);
            this.label5.TabIndex = 23;
            this.label5.Text = "SFTP Pass";
            // 
            // textBox_SFTPPass
            // 
            this.textBox_SFTPPass.AllowDrop = true;
            this.textBox_SFTPPass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_SFTPPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_SFTPPass.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_SFTPPass.Location = new System.Drawing.Point(396, 88);
            this.textBox_SFTPPass.Name = "textBox_SFTPPass";
            this.textBox_SFTPPass.ReadOnly = true;
            this.textBox_SFTPPass.Size = new System.Drawing.Size(150, 11);
            this.textBox_SFTPPass.TabIndex = 7;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(284, 135);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 27;
            this.label7.Text = "App User Pass";
            // 
            // textBox_AppUserPass
            // 
            this.textBox_AppUserPass.AllowDrop = true;
            this.textBox_AppUserPass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_AppUserPass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_AppUserPass.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_AppUserPass.Location = new System.Drawing.Point(396, 136);
            this.textBox_AppUserPass.Name = "textBox_AppUserPass";
            this.textBox_AppUserPass.ReadOnly = true;
            this.textBox_AppUserPass.Size = new System.Drawing.Size(150, 11);
            this.textBox_AppUserPass.TabIndex = 9;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(284, 111);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(82, 13);
            this.label8.TabIndex = 25;
            this.label8.Text = "App User Name";
            // 
            // textBox_AppUserName
            // 
            this.textBox_AppUserName.AllowDrop = true;
            this.textBox_AppUserName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_AppUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_AppUserName.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_AppUserName.Location = new System.Drawing.Point(396, 112);
            this.textBox_AppUserName.Name = "textBox_AppUserName";
            this.textBox_AppUserName.ReadOnly = true;
            this.textBox_AppUserName.Size = new System.Drawing.Size(150, 11);
            this.textBox_AppUserName.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(284, 63);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 29;
            this.label9.Text = "Cluster Security Pass";
            this.label9.Click += new System.EventHandler(this.label9_Click);
            // 
            // textBox_ClusterSecurity
            // 
            this.textBox_ClusterSecurity.AllowDrop = true;
            this.textBox_ClusterSecurity.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.textBox_ClusterSecurity.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_ClusterSecurity.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox_ClusterSecurity.Location = new System.Drawing.Point(396, 64);
            this.textBox_ClusterSecurity.Name = "textBox_ClusterSecurity";
            this.textBox_ClusterSecurity.ReadOnly = true;
            this.textBox_ClusterSecurity.Size = new System.Drawing.Size(150, 11);
            this.textBox_ClusterSecurity.TabIndex = 6;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(172, 6);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(175, 22);
            this.toolStripMenuItem3.Text = "&Decode Passphrase";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.toolStripMenuItem3_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItem3});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ScrollBar;
            this.ClientSize = new System.Drawing.Size(572, 167);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.textBox_ClusterSecurity);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.textBox_AppUserPass);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.textBox_AppUserName);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox_SFTPPass);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_LocalAdminPass);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_LocalAdminName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_UCProduct);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_UCVersion);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "UCOS Password Decrypter";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TextBox textBox_UCVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_UCProduct;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_LocalAdminName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox textBox_LocalAdminPass;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox_SFTPPass;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_AppUserPass;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBox_AppUserName;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBox_ClusterSecurity;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
    }
}

