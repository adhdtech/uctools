namespace DRSBackupDecrypter
{
    partial class UpdateSecurityPasswordForm
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
            this.tbCurrentSecurityPassword = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbNewSecurityPassword = new System.Windows.Forms.TextBox();
            this.tbSHA1Warning = new System.Windows.Forms.TextBox();
            this.btnCreateXML = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.tbNewXMLFilePath = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // tbCurrentSecurityPassword
            // 
            this.tbCurrentSecurityPassword.Location = new System.Drawing.Point(148, 12);
            this.tbCurrentSecurityPassword.Name = "tbCurrentSecurityPassword";
            this.tbCurrentSecurityPassword.ReadOnly = true;
            this.tbCurrentSecurityPassword.Size = new System.Drawing.Size(201, 20);
            this.tbCurrentSecurityPassword.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(131, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Current Security Password";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 42);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(119, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "New Security Password";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // tbNewSecurityPassword
            // 
            this.tbNewSecurityPassword.Location = new System.Drawing.Point(148, 39);
            this.tbNewSecurityPassword.Name = "tbNewSecurityPassword";
            this.tbNewSecurityPassword.Size = new System.Drawing.Size(201, 20);
            this.tbNewSecurityPassword.TabIndex = 3;
            this.tbNewSecurityPassword.TextChanged += new System.EventHandler(this.tbNewSecurityPassword_TextChanged);
            // 
            // tbSHA1Warning
            // 
            this.tbSHA1Warning.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbSHA1Warning.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSHA1Warning.Location = new System.Drawing.Point(12, 101);
            this.tbSHA1Warning.Multiline = true;
            this.tbSHA1Warning.Name = "tbSHA1Warning";
            this.tbSHA1Warning.ReadOnly = true;
            this.tbSHA1Warning.Size = new System.Drawing.Size(357, 49);
            this.tbSHA1Warning.TabIndex = 5;
            this.tbSHA1Warning.Text = "If the current security password is not known, a SHA1 hash will have to be calcul" +
    "ated for each TAR file.  This can take some time for larger files.";
            this.tbSHA1Warning.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // btnCreateXML
            // 
            this.btnCreateXML.Enabled = false;
            this.btnCreateXML.Location = new System.Drawing.Point(357, 65);
            this.btnCreateXML.Name = "btnCreateXML";
            this.btnCreateXML.Size = new System.Drawing.Size(75, 22);
            this.btnCreateXML.TabIndex = 6;
            this.btnCreateXML.Text = "Create XML";
            this.btnCreateXML.UseVisualStyleBackColor = true;
            this.btnCreateXML.Click += new System.EventHandler(this.btnCreateXML_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "New XML File";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // tbNewXMLFilePath
            // 
            this.tbNewXMLFilePath.Location = new System.Drawing.Point(148, 67);
            this.tbNewXMLFilePath.Name = "tbNewXMLFilePath";
            this.tbNewXMLFilePath.Size = new System.Drawing.Size(201, 20);
            this.tbNewXMLFilePath.TabIndex = 8;
            // 
            // UpdateSecurityPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 162);
            this.Controls.Add(this.tbNewXMLFilePath);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnCreateXML);
            this.Controls.Add(this.tbSHA1Warning);
            this.Controls.Add(this.tbNewSecurityPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tbCurrentSecurityPassword);
            this.Name = "UpdateSecurityPasswordForm";
            this.Text = "Update Security Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox tbCurrentSecurityPassword;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbNewSecurityPassword;
        private System.Windows.Forms.TextBox tbSHA1Warning;
        private System.Windows.Forms.Button btnCreateXML;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbNewXMLFilePath;
    }
}