namespace DRSBackupDecrypter
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem_File_ChangeSecurityPassword = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lblClustSecPw = new System.Windows.Forms.Label();
            this.txtClustSecPw = new System.Windows.Forms.TextBox();
            this.lblDecryptPass = new System.Windows.Forms.Label();
            this.chkDecryptPass = new System.Windows.Forms.CheckBox();
            this.btnSelectOutputDirectory = new System.Windows.Forms.Button();
            this.btnSelectBackupSet = new System.Windows.Forms.Button();
            this.lblOutputDirectory = new System.Windows.Forms.Label();
            this.txtOutputDirectory = new System.Windows.Forms.TextBox();
            this.lblBackupSet = new System.Windows.Forms.Label();
            this.txtBackupSet = new System.Windows.Forms.TextBox();
            this.btnDecrypt = new System.Windows.Forms.Button();
            this.lblUCVersion = new System.Windows.Forms.Label();
            this.txtUCVersion = new System.Windows.Forms.TextBox();
            this.clb_DecryptFilesSelection = new System.Windows.Forms.CheckedListBox();
            this.toolStripMenuItem_File_OpenBackupSet = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(495, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem_File_OpenBackupSet,
            this.toolStripMenuItem_File_ChangeSecurityPassword,
            this.toolStripSeparator,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // toolStripMenuItem_File_ChangeSecurityPassword
            // 
            this.toolStripMenuItem_File_ChangeSecurityPassword.Enabled = false;
            this.toolStripMenuItem_File_ChangeSecurityPassword.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem_File_ChangeSecurityPassword.Name = "toolStripMenuItem_File_ChangeSecurityPassword";
            this.toolStripMenuItem_File_ChangeSecurityPassword.Size = new System.Drawing.Size(217, 26);
            this.toolStripMenuItem_File_ChangeSecurityPassword.Text = "Change Security Password";
            this.toolStripMenuItem_File_ChangeSecurityPassword.Click += new System.EventHandler(this.toolStripMenuItem_File_ChangeSecurityPassword_Click);
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(214, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(217, 26);
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
            this.openFileDialog1.Filter = "DRS Backup Set|*_drfComponent.xml";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lblClustSecPw);
            this.splitContainer1.Panel1.Controls.Add(this.txtClustSecPw);
            this.splitContainer1.Panel1.Controls.Add(this.lblDecryptPass);
            this.splitContainer1.Panel1.Controls.Add(this.chkDecryptPass);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectOutputDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.btnSelectBackupSet);
            this.splitContainer1.Panel1.Controls.Add(this.lblOutputDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.txtOutputDirectory);
            this.splitContainer1.Panel1.Controls.Add(this.lblBackupSet);
            this.splitContainer1.Panel1.Controls.Add(this.txtBackupSet);
            this.splitContainer1.Panel1.Controls.Add(this.btnDecrypt);
            this.splitContainer1.Panel1.Controls.Add(this.lblUCVersion);
            this.splitContainer1.Panel1.Controls.Add(this.txtUCVersion);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.clb_DecryptFilesSelection);
            this.splitContainer1.Size = new System.Drawing.Size(495, 262);
            this.splitContainer1.SplitterDistance = 124;
            this.splitContainer1.TabIndex = 23;
            // 
            // lblClustSecPw
            // 
            this.lblClustSecPw.AutoSize = true;
            this.lblClustSecPw.Location = new System.Drawing.Point(8, 85);
            this.lblClustSecPw.Name = "lblClustSecPw";
            this.lblClustSecPw.Size = new System.Drawing.Size(103, 13);
            this.lblClustSecPw.TabIndex = 35;
            this.lblClustSecPw.Text = "Guess Clust Sec Pw";
            // 
            // txtClustSecPw
            // 
            this.txtClustSecPw.AllowDrop = true;
            this.txtClustSecPw.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClustSecPw.Location = new System.Drawing.Point(126, 84);
            this.txtClustSecPw.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtClustSecPw.Name = "txtClustSecPw";
            this.txtClustSecPw.ReadOnly = true;
            this.txtClustSecPw.Size = new System.Drawing.Size(262, 18);
            this.txtClustSecPw.TabIndex = 34;
            this.txtClustSecPw.TextChanged += new System.EventHandler(this.txtClustSecPw_TextChanged);
            // 
            // lblDecryptPass
            // 
            this.lblDecryptPass.AutoSize = true;
            this.lblDecryptPass.Location = new System.Drawing.Point(300, 64);
            this.lblDecryptPass.Name = "lblDecryptPass";
            this.lblDecryptPass.Size = new System.Drawing.Size(70, 13);
            this.lblDecryptPass.TabIndex = 33;
            this.lblDecryptPass.Text = "Decrypt Pass";
            // 
            // chkDecryptPass
            // 
            this.chkDecryptPass.AutoSize = true;
            this.chkDecryptPass.Enabled = false;
            this.chkDecryptPass.Location = new System.Drawing.Point(374, 64);
            this.chkDecryptPass.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkDecryptPass.Name = "chkDecryptPass";
            this.chkDecryptPass.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkDecryptPass.Size = new System.Drawing.Size(15, 14);
            this.chkDecryptPass.TabIndex = 32;
            this.chkDecryptPass.UseVisualStyleBackColor = true;
            this.chkDecryptPass.CheckedChanged += new System.EventHandler(this.chkDecryptPass_CheckedChanged);
            // 
            // btnSelectOutputDirectory
            // 
            this.btnSelectOutputDirectory.Enabled = false;
            this.btnSelectOutputDirectory.Location = new System.Drawing.Point(405, 35);
            this.btnSelectOutputDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectOutputDirectory.Name = "btnSelectOutputDirectory";
            this.btnSelectOutputDirectory.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSelectOutputDirectory.Size = new System.Drawing.Size(64, 20);
            this.btnSelectOutputDirectory.TabIndex = 31;
            this.btnSelectOutputDirectory.Text = "Select";
            this.btnSelectOutputDirectory.UseVisualStyleBackColor = true;
            this.btnSelectOutputDirectory.Click += new System.EventHandler(this.btnSelectOutputDirectory_Click);
            // 
            // btnSelectBackupSet
            // 
            this.btnSelectBackupSet.Location = new System.Drawing.Point(405, 11);
            this.btnSelectBackupSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectBackupSet.Name = "btnSelectBackupSet";
            this.btnSelectBackupSet.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnSelectBackupSet.Size = new System.Drawing.Size(64, 20);
            this.btnSelectBackupSet.TabIndex = 30;
            this.btnSelectBackupSet.Text = "Select";
            this.btnSelectBackupSet.UseVisualStyleBackColor = true;
            this.btnSelectBackupSet.Click += new System.EventHandler(this.btnSelectBackupSet_Click);
            // 
            // lblOutputDirectory
            // 
            this.lblOutputDirectory.AutoSize = true;
            this.lblOutputDirectory.Location = new System.Drawing.Point(8, 36);
            this.lblOutputDirectory.Name = "lblOutputDirectory";
            this.lblOutputDirectory.Size = new System.Drawing.Size(84, 13);
            this.lblOutputDirectory.TabIndex = 29;
            this.lblOutputDirectory.Text = "Output Directory";
            // 
            // txtOutputDirectory
            // 
            this.txtOutputDirectory.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutputDirectory.Location = new System.Drawing.Point(126, 36);
            this.txtOutputDirectory.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtOutputDirectory.Name = "txtOutputDirectory";
            this.txtOutputDirectory.ReadOnly = true;
            this.txtOutputDirectory.Size = new System.Drawing.Size(262, 18);
            this.txtOutputDirectory.TabIndex = 28;
            this.txtOutputDirectory.TextChanged += new System.EventHandler(this.txtOutputDirectory_TextChanged);
            // 
            // lblBackupSet
            // 
            this.lblBackupSet.AutoSize = true;
            this.lblBackupSet.Location = new System.Drawing.Point(8, 13);
            this.lblBackupSet.Name = "lblBackupSet";
            this.lblBackupSet.Size = new System.Drawing.Size(63, 13);
            this.lblBackupSet.TabIndex = 27;
            this.lblBackupSet.Text = "Backup Set";
            // 
            // txtBackupSet
            // 
            this.txtBackupSet.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBackupSet.Location = new System.Drawing.Point(126, 12);
            this.txtBackupSet.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBackupSet.Name = "txtBackupSet";
            this.txtBackupSet.ReadOnly = true;
            this.txtBackupSet.Size = new System.Drawing.Size(262, 18);
            this.txtBackupSet.TabIndex = 26;
            // 
            // btnDecrypt
            // 
            this.btnDecrypt.Enabled = false;
            this.btnDecrypt.Location = new System.Drawing.Point(405, 59);
            this.btnDecrypt.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnDecrypt.Name = "btnDecrypt";
            this.btnDecrypt.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.btnDecrypt.Size = new System.Drawing.Size(64, 20);
            this.btnDecrypt.TabIndex = 25;
            this.btnDecrypt.Text = "Decrypt";
            this.btnDecrypt.UseVisualStyleBackColor = true;
            this.btnDecrypt.Click += new System.EventHandler(this.btnDecrypt_Click);
            // 
            // lblUCVersion
            // 
            this.lblUCVersion.AutoSize = true;
            this.lblUCVersion.Location = new System.Drawing.Point(8, 60);
            this.lblUCVersion.Name = "lblUCVersion";
            this.lblUCVersion.Size = new System.Drawing.Size(60, 13);
            this.lblUCVersion.TabIndex = 24;
            this.lblUCVersion.Text = "UC Version";
            // 
            // txtUCVersion
            // 
            this.txtUCVersion.AllowDrop = true;
            this.txtUCVersion.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUCVersion.Location = new System.Drawing.Point(126, 60);
            this.txtUCVersion.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtUCVersion.Name = "txtUCVersion";
            this.txtUCVersion.ReadOnly = true;
            this.txtUCVersion.Size = new System.Drawing.Size(150, 18);
            this.txtUCVersion.TabIndex = 23;
            // 
            // clb_DecryptFilesSelection
            // 
            this.clb_DecryptFilesSelection.CheckOnClick = true;
            this.clb_DecryptFilesSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clb_DecryptFilesSelection.FormattingEnabled = true;
            this.clb_DecryptFilesSelection.Location = new System.Drawing.Point(0, 0);
            this.clb_DecryptFilesSelection.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.clb_DecryptFilesSelection.Name = "clb_DecryptFilesSelection";
            this.clb_DecryptFilesSelection.Size = new System.Drawing.Size(495, 134);
            this.clb_DecryptFilesSelection.TabIndex = 18;
            this.clb_DecryptFilesSelection.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBox1_ItemCheck);
            this.clb_DecryptFilesSelection.SelectedIndexChanged += new System.EventHandler(this.clb_DecryptFilesSelection_SelectedIndexChanged);
            this.clb_DecryptFilesSelection.KeyUp += new System.Windows.Forms.KeyEventHandler(this.checkedListBox1_KeyUp);
            this.clb_DecryptFilesSelection.MouseUp += new System.Windows.Forms.MouseEventHandler(this.checkedListBox1_MouseUp);
            // 
            // toolStripMenuItem_File_OpenBackupSet
            // 
            this.toolStripMenuItem_File_OpenBackupSet.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem_File_OpenBackupSet.Image")));
            this.toolStripMenuItem_File_OpenBackupSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripMenuItem_File_OpenBackupSet.Name = "toolStripMenuItem_File_OpenBackupSet";
            this.toolStripMenuItem_File_OpenBackupSet.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.toolStripMenuItem_File_OpenBackupSet.Size = new System.Drawing.Size(217, 26);
            this.toolStripMenuItem_File_OpenBackupSet.Text = "&Open Backup Set";
            this.toolStripMenuItem_File_OpenBackupSet.Click += new System.EventHandler(this.toolStripMenuItem_File_OpenBackupSet_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 286);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainForm";
            this.Text = "DRS Backup Decrypter";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_ChangeSecurityPassword;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label lblClustSecPw;
        private System.Windows.Forms.TextBox txtClustSecPw;
        private System.Windows.Forms.Label lblDecryptPass;
        private System.Windows.Forms.CheckBox chkDecryptPass;
        private System.Windows.Forms.Button btnSelectOutputDirectory;
        private System.Windows.Forms.Button btnSelectBackupSet;
        private System.Windows.Forms.Label lblOutputDirectory;
        private System.Windows.Forms.TextBox txtOutputDirectory;
        private System.Windows.Forms.Label lblBackupSet;
        private System.Windows.Forms.TextBox txtBackupSet;
        private System.Windows.Forms.Button btnDecrypt;
        private System.Windows.Forms.Label lblUCVersion;
        private System.Windows.Forms.TextBox txtUCVersion;
        private System.Windows.Forms.CheckedListBox clb_DecryptFilesSelection;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem_File_OpenBackupSet;
    }
}

