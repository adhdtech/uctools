using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DRSBackupDecrypter
{
    public partial class UpdateSecurityPasswordForm : Form
    {
        public UpdateSecurityPasswordForm()
        {
            InitializeComponent();
            string sNewXMLFilePath = DRSD.myBackupSet._sBackupDirectory + @"\" + DRSD.myBackupSet._sBackupXMLFile + @".new";
            tbNewXMLFilePath.Text = sNewXMLFilePath;
            if (DRSD.myBackupSet.HaveClusterSecurityPw) {
                tbCurrentSecurityPassword.Text = DRSD.myBackupSet._sClusterSecurityPw;
            }
            this.ActiveControl = tbNewSecurityPassword;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void tbNewSecurityPassword_TextChanged(object sender, EventArgs e)
        {
            CheckReady();
        }

        private void btnCreateXML_Click(object sender, EventArgs e)
        {
            btnCreateXML.Enabled = false;
            bool bGenXMLSucceeded = DRSD.myBackupSet.UpdateXMLSecurityPassword(tbNewSecurityPassword.Text, tbNewXMLFilePath.Text);
            if (bGenXMLSucceeded)
            {
                MessageBox.Show("Successfully created new XML file");
            }
            else {
                MessageBox.Show("Could not create XML: " + DRSD.myBackupSet._sErrorMsg);
            }
            btnCreateXML.Enabled = true;
        }

        private void CheckReady() {
            if (tbNewSecurityPassword.Text.Length >= 10 &&
                tbNewSecurityPassword.Text.Length <= 20 &&
                Directory.Exists(Path.GetDirectoryName(tbNewXMLFilePath.Text))
               )
            {
                btnCreateXML.Enabled = true;
            }
            else
            {
                btnCreateXML.Enabled = false;
            }
        }
    }
}
