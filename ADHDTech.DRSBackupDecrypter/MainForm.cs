using System;
using System.IO;
using System.Xml;
using System.Text;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Net.NetworkInformation;
using ADHDTech.CiscoCrypt;

namespace DRSBackupDecrypter
{


    public partial class MainForm : Form
    {
        Form DecryptProgressBox = new DRSBackupDecrypter.DecryptProgress();

        public MainForm()
        {
            InitializeComponent();
            Shown += new EventHandler(MainForm_Shown);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void MainForm_Shown(object sender, EventArgs e)
        {

        }

        private void SelectBackupSet()
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                chkDecryptPass.Checked = false;
                btnDecrypt.Enabled = false;
                DRSD.myBackupSet = new DRSBackupSet(openFileDialog1.FileName);
                if (DRSD.myBackupSet.XMLLoaded)
                {

                    if (DRSD.myBackupSet.IsEncrypted)
                    {
                        string sUCMVersionMajor = DRSD.myBackupSet._oBackupSetDef.FeatureObjects.First().Version.Substring(0, 4);

                        if (String.Equals(sUCMVersionMajor, "11.0") && !File.Exists("KeyHashGenerator.jar"))
                        {
                            string sMessage = "UCOS 11.0 backups require the KeyHashGenerator.jar file in order to verify or update the security password.  Download it now?";
                            if (MessageBox.Show(
                                                    sMessage, "Missing KeyHashGenerator.jar", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk
                                                ) == DialogResult.Yes)
                            {
                                System.Diagnostics.Process.Start("https://www.adhdtech.com/KeyHashGenerator.jar");
                            }
                        }

                        toolStripMenuItem_File_ChangeSecurityPassword.Enabled = true;
                        chkDecryptPass.Checked = DRSD.myBackupSet.HaveRandomBackupPass;
                        txtUCVersion.Text = DRSD.myBackupSet._oBackupSetDef.FeatureObjects.First().Version;
                        DRSD.myBackupSet.VerifyTarFiles();

                        txtOutputDirectory.ReadOnly = false;
                        btnDecrypt.Enabled = ReadyToDecrypt();

                        txtClustSecPw.ReadOnly = false;
                        txtClustSecPw.Text = "";
                        txtClustSecPw.BackColor = Color.LightPink;
                        DRSD.sBackupSetDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                        DRSD.sBackupSetXMLFilename = Path.GetFileName(openFileDialog1.FileName);
                        txtBackupSet.Text = DRSD.sBackupSetDirectory + "\\" + DRSD.sBackupSetXMLFilename;
                        //textBox5.ReadOnly = false;
                        btnSelectOutputDirectory.Enabled = true;
                        clb_DecryptFilesSelection.Items.Clear();

                        foreach (KeyValuePair<string, TARFileObj> oTARRef in DRSD.myBackupSet._dTARFiles)
                        {
                            if (oTARRef.Value._bFileExists)
                            {
                                clb_DecryptFilesSelection.Items.Add(oTARRef.Value._sFileName);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Backup set does not appear to be encrypted!");
                    }
                }
                else
                {
                    MessageBox.Show("XML not loaded: " + DRSD.myBackupSet.ErrorMsg);
                }
                /*
                DRSD.sXMLEncryptKey = "";
                DRSD.sUCVersion = "";
                DRSD.sFilenameEncrypted = openFileDialog1.FileName;
                // Create an XML reader for this file.
                using (XmlReader reader = XmlReader.Create(DRSD.sFilenameEncrypted))
                {
                    if (reader.ReadToFollowing("Version"))
                    {
                        DRSD.sUCVersion = reader.ReadElementContentAsString();
                        txtUCVersion.Text = DRSD.sUCVersion;
                        string MajorVersion = DRSD.sUCVersion.Substring(0, 1);

                        if (reader.ReadToFollowing("EncryptKey"))
                        {
                            DRSD.sXMLEncryptKey = reader.ReadElementContentAsString();
                        }
                        else
                        {
                            MessageBox.Show("Backup set not encrypted");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Could not find version");
                    }
                }
                txtClustSecPw.ReadOnly = false;
                txtClustSecPw.Text = "";
                txtClustSecPw.BackColor = Color.LightPink;
                DRSD.sBackupSetDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                DRSD.sBackupSetXMLFilename = Path.GetFileName(openFileDialog1.FileName);
                txtBackupSet.Text = DRSD.sBackupSetDirectory + "\\" + DRSD.sBackupSetXMLFilename;
                //textBox5.ReadOnly = false;
                btnSelectOutputDirectory.Enabled = true;
                clb_DecryptFilesSelection.Items.Clear();
                string[] tarFiles = Directory.GetFiles(DRSD.sBackupSetDirectory, "*.tar");
                string clmFile = "";
                for (int f = 0; f < tarFiles.Count(); f++ )
                {
                    string sTARFilename = Path.GetFileName(tarFiles[f]);
                    if ((sTARFilename.Length > 24) && (String.Compare(sTARFilename, 0, DRSD.sBackupSetXMLFilename, 0, 20) == 0))
                    {
                        string fileName = Path.GetFileName(tarFiles[f]);
                        clb_DecryptFilesSelection.Items.Add(fileName);

                        // Need to add logic to this check; if the backup set was generated using
                        // the new method that does not include the random backup password, we do
                        // not need to bother with this step.
                        //
                        // OR - use this step to determine whether or not the new method is being
                        // used.  If we find the CLM file but it does not have the random backup
                        // password, assume we're using the new method.
                        //
                        // May also use the file size to determine whether or not we have the
                        // random backup password attached.  Should it be a multiple of X kb?
                        if (clmFile.Length == 0 && String.Equals(fileName.Substring(fileName.Length-8, 8),"_CLM.tar"))
                        {
                            DRSD.sRandomBackupPass = System.Text.Encoding.UTF8.GetString(ADHDTech.PasswordUtil.GetRandomPasswordFromFile(DRSD.sBackupSetDirectory + "\\" + fileName));
                            chkDecryptPass.Checked = true;
                            btnDecrypt.Enabled = ReadyToDecrypt();
                        }
                    }
                }
                */
            }
        }

        private bool ReadyToDecrypt()
        {
            bool bReady = false;
            if (DRSD.sOutputDirectory.Length > 0 &&
                clb_DecryptFilesSelection.CheckedItems.Count > 0 &&
                DRSD.myBackupSet.HaveRandomBackupPass &&
                chkDecryptPass.Checked == true)
                bReady = true;
            return bReady;
        }

        private void SelectOutputDir()
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                if (folderBrowserDialog1.SelectedPath != DRSD.sBackupSetDirectory)
                {
                    DRSD.sOutputDirectory = folderBrowserDialog1.SelectedPath;
                    txtOutputDirectory.Text = DRSD.sOutputDirectory;
                }
                else
                {
                    MessageBox.Show("The backups cannot be decrypted to the source directory!");
                }
            }
        }

        private void btnDecrypt_Click(object sender, EventArgs e)
        {
            if ((DRSD.sOutputDirectory.Length > 0) && (clb_DecryptFilesSelection.CheckedItems.Count > 0) && (chkDecryptPass.Checked == true))
            {
                DRSD.sFilesToDecrypt = clb_DecryptFilesSelection.CheckedItems.OfType<String>().ToArray();
                DecryptProgressBox.ShowDialog();
            }
            else
            {
                MessageBox.Show("Must select target directory and file(s) to decrypt!");
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form aboutbox = new Form();
            Label label1 = new Label();
            Label label2 = new Label();
            Label label3 = new Label();
            aboutbox.Height = 175;
            aboutbox.Width = 350;
            label1.AutoSize = true;
            label2.AutoSize = true;
            label3.AutoSize = true;
            Font myFnt1 = new Font("Veranda", 12, FontStyle.Bold);
            Font myFnt2 = new Font("Veranda", 10);
            label1.Font = myFnt1;
            label2.Font = myFnt2;
            label3.Font = myFnt2;
            label1.Text = "DRS Backup Decrypter Utility";
            label2.Text = "By: Pete Brown (jpbrown@adhdtech.com)";
            label3.Text = "Version 1.5a\nBuild 20171028";
            aboutbox.Controls.Add(label1);
            aboutbox.Controls.Add(label2);
            aboutbox.Controls.Add(label3);
            label1.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 25);
            label2.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 50);
            label3.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 75);
            aboutbox.ShowDialog();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem_File_OpenBackupSet_Click(object sender, EventArgs e)
        {
            SelectBackupSet();
        }

        private void toolStripMenuItem_File_ChangeSecurityPassword_Click(object sender, EventArgs e)
        {
            // User wants to change the XML password

            if (!DRSD.myBackupSet.ReadyToUpdateXMLSecurityPassword())
            {
                MessageBox.Show(DRSD.myBackupSet._sErrorMsg);
            }
            else
            {
                //DRSD.myBackupSet.VerifyTarFiles();
                Form UpdateSecurityPasswordBox = new DRSBackupDecrypter.UpdateSecurityPasswordForm();
                UpdateSecurityPasswordBox.ShowDialog();
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnSelectBackupSet_Click(object sender, EventArgs e)
        {
            SelectBackupSet();
        }

        private void btnSelectOutputDirectory_Click(object sender, EventArgs e)
        {
            SelectOutputDir();
            btnDecrypt.Enabled = ReadyToDecrypt();
        }

        private void chkDecryptPass_CheckedChanged(object sender, EventArgs e)
        {
            btnDecrypt.Enabled = ReadyToDecrypt();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button1.Enabled = ReadyToDecrypt();
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            btnDecrypt.Enabled = ReadyToDecrypt();
        }

        private void checkedListBox1_KeyUp(object sender, KeyEventArgs e)
        {
            btnDecrypt.Enabled = ReadyToDecrypt();
        }

        private void checkedListBox1_MouseUp(object sender, MouseEventArgs e)
        {
            btnDecrypt.Enabled = ReadyToDecrypt();
        }

        private void txtClustSecPw_TextChanged(object sender, EventArgs e)
        {
            DRSD.sClusterSecurityPass = txtClustSecPw.Text;
            bool isPasswordValid = DRSD.myBackupSet.SetPassword(DRSD.sClusterSecurityPass);
            if (isPasswordValid)
            {
                txtClustSecPw.BackColor = Color.LightGreen;
                DRSD.sRandomBackupPass = DRSD.myBackupSet._sRandomBackupPass;
                chkDecryptPass.Checked = true;
                btnDecrypt.Enabled = ReadyToDecrypt();
                if (!DRSD.myBackupSet._bAllFilesReadyForDecrypt)
                {
                    DRSD.myBackupSet.VerifyTarFiles();
                }
            }
            else
            {
                txtClustSecPw.BackColor = Color.LightPink;
            }
        }

        private void clb_DecryptFilesSelection_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtOutputDirectory_TextChanged(object sender, EventArgs e)
        {
            if (Directory.Exists(txtOutputDirectory.Text) && txtOutputDirectory.Text != DRSD.sBackupSetDirectory)
            {
                DRSD.sOutputDirectory = txtOutputDirectory.Text;
                btnDecrypt.Enabled = ReadyToDecrypt();
            }
            else
            {
                DRSD.sOutputDirectory = "";
                btnDecrypt.Enabled = ReadyToDecrypt();
            }
        }
    }
}
