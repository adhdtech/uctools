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

namespace WindowsFormsApplication1
{

    public partial class Form1 : Form
    {
        Form AboutBox = new DRSBackupDecrypter.AboutBox1();
        UCOSHostCfg myUCSHostCfg = new UCOSHostCfg();

        public Form1()
        {
            InitializeComponent();
            Shown += new EventHandler(Form1_Shown);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Shown(object sender, EventArgs e)
        {
        }

        private void SelectPlatformConfigFile() {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                ADHDTech.CiscoCrypt.platformConfigXML.PlatformData oPlatformConfig = ADHDTech.CiscoCrypt.Functions.LoadPlatformConfigFile(openFileDialog1.FileName);
                DRSD.sUCVersion = oPlatformConfig.Version;
                DRSD.sUCProduct = oPlatformConfig.ProductDeployment.First().ParamValue;
                DRSD.sLocalHostName = oPlatformConfig.LocalHostName.First().ParamValue;
                DRSD.sLocalHostIP0 = oPlatformConfig.LocalHostIP0.First().ParamValue;
                DRSD.sLocalHostAdminName = oPlatformConfig.LocalHostAdminName.First().ParamValue;
                DRSD.sLocalHostAdminPwCrypt = oPlatformConfig.LocalHostAdminPwCrypt.First().ParamValue;
                DRSD.sSftpPwCrypt = oPlatformConfig.SftpPwCrypt.First().ParamValue;
                DRSD.sIPSecSecurityPwCrypt = oPlatformConfig.SftpPwCrypt.First().ParamValue;
                DRSD.sApplUserUsername = oPlatformConfig.ApplUserUsername.First().ParamValue;
                DRSD.sApplUserPwCrypt = oPlatformConfig.ApplUserPwCrypt.First().ParamValue;


                //textBox2.ReadOnly = false;
                DRSD.sBackupSetDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                DRSD.sBackupSetXMLFilename = Path.GetFileName(openFileDialog1.FileName);
                textBox3.Text = DRSD.sBackupSetDirectory + "\\" + DRSD.sBackupSetXMLFilename;

                ADHDTech.CiscoCrypt.PlatformConfigPassword oPlatformConfigDecrypter = new ADHDTech.CiscoCrypt.PlatformConfigPassword();
                

                textBox_UCVersion.Text = DRSD.sUCVersion;
                textBox_UCProduct.Text = DRSD.sUCProduct;
                textBox_LocalAdminName.Text = DRSD.sLocalHostAdminName;
                textBox_LocalAdminPass.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sLocalHostAdminPwCrypt);
                //textBox_LocalAdminPass.Text = ADHDTech.CiscoCrypt.Functions.DecryptCCMPlatformValue(DRSD.sLocalHostAdminPwCrypt, "49c8182574a74ca2ddc4358024e98b6e02edfb5a54e3453b7a8e3db5a697bb19");
                textBox_SFTPPass.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sSftpPwCrypt);
                textBox_ClusterSecurity.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sIPSecSecurityPwCrypt);
                textBox_AppUserName.Text = DRSD.sApplUserUsername;
                textBox_AppUserPass.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sApplUserPwCrypt);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            /*
            DRSD.sClusterSecurityPass = textBox2.Text;
            DRSD.sClusterSecurityPassEncHex = EncryptPass(DRSD.sClusterSecurityPass);
            DRSD.sRandomBackupPass = GetRandomBackupPass(DRSD.sClusterSecurityPassEncHex, DRSD.sXMLEncryptKey);
            if (DRSD.sRandomBackupPass.Length == 20)
            {
                button1.Enabled = true;
                textBox2.BackColor = Color.LightGreen;
            }
            else
            {
                button1.Enabled = false;
                textBox2.BackColor = Color.LightPink;
            }
             */
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

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
            label1.Text = "UCOS Password Decrypter";
            label2.Text = "By: Pete Brown (jpbrown@adhdtech.com)";
            label3.Text = "Version 1.5a\nBuild 20171031";
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

        private void label4_Click_1(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SelectPlatformConfigFile();
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            SelectPlatformConfigFile();
        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_2(object sender, EventArgs e)
        {

        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //button1.Enabled = ReadyToDecrypt();
        }

        private void label2_Click_3(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            // Open passphrase decode window
            ADHDTech.CiscoCrypt.DecodePassphraseForm oPassphraseDecodeForm = new ADHDTech.CiscoCrypt.DecodePassphraseForm();
            oPassphraseDecodeForm.ShowDialog();
        }

        private void FormSelectUCOSHost_Closed(object sender, EventArgs e)
        {
            if (myUCSHostCfg.sUCOSHost.Length > 0 && myUCSHostCfg.sUCOSRemoteUser.Length > 0 && myUCSHostCfg.sUCOSPassphrase.Length > 8) {
                // Let's try to open the security files
                ADHDTech.CiscoSCP.UCOSClientSFTP testClient = new ADHDTech.CiscoSCP.UCOSClientSFTP(myUCSHostCfg.sUCOSHost, myUCSHostCfg.sUCOSRemoteUser, myUCSHostCfg.sUCOSPassphrase);
                Dictionary<string, byte[]> filePack = testClient.GetSecurityFilePack();

                ADHDTech.CiscoCrypt.platformConfigXML.PlatformData oPlatformConfig = ADHDTech.CiscoCrypt.Functions.LoadPlatformConfigBytes(filePack[@"/usr/local/platform/conf/platformConfig.xml"]);
                DRSD.sUCVersion = oPlatformConfig.Version;
                DRSD.sUCProduct = oPlatformConfig.ProductDeployment.First().ParamValue;
                DRSD.sLocalHostName = oPlatformConfig.LocalHostName.First().ParamValue;
                DRSD.sLocalHostIP0 = oPlatformConfig.LocalHostIP0.First().ParamValue;
                DRSD.sLocalHostAdminName = oPlatformConfig.LocalHostAdminName.First().ParamValue;
                DRSD.sLocalHostAdminPwCrypt = oPlatformConfig.LocalHostAdminPwCrypt.First().ParamValue;
                DRSD.sSftpPwCrypt = oPlatformConfig.SftpPwCrypt.First().ParamValue;
                DRSD.sIPSecSecurityPwCrypt = oPlatformConfig.SftpPwCrypt.First().ParamValue;
                DRSD.sApplUserUsername = oPlatformConfig.ApplUserUsername.First().ParamValue;
                DRSD.sApplUserPwCrypt = oPlatformConfig.ApplUserPwCrypt.First().ParamValue;


                //textBox2.ReadOnly = false;
                //DRSD.sBackupSetDirectory = Path.GetDirectoryName(openFileDialog1.FileName);
                //DRSD.sBackupSetXMLFilename = Path.GetFileName(openFileDialog1.FileName);
                //textBox3.Text = DRSD.sBackupSetDirectory + "\\" + DRSD.sBackupSetXMLFilename;
                textBox3.Text = @"/usr/local/platform/conf/platformConfig.xml";

                ADHDTech.CiscoCrypt.PlatformConfigPassword oPlatformConfigDecrypter = new ADHDTech.CiscoCrypt.PlatformConfigPassword();


                textBox_UCVersion.Text = DRSD.sUCVersion;
                textBox_UCProduct.Text = DRSD.sUCProduct;
                textBox_LocalAdminName.Text = DRSD.sLocalHostAdminName;
                textBox_LocalAdminPass.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sLocalHostAdminPwCrypt);
                //textBox_LocalAdminPass.Text = ADHDTech.CiscoCrypt.Functions.DecryptCCMPlatformValue(DRSD.sLocalHostAdminPwCrypt, "49c8182574a74ca2ddc4358024e98b6e02edfb5a54e3453b7a8e3db5a697bb19");
                textBox_SFTPPass.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sSftpPwCrypt);
                textBox_ClusterSecurity.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sIPSecSecurityPwCrypt);
                textBox_AppUserName.Text = DRSD.sApplUserUsername;
                textBox_AppUserPass.Text = oPlatformConfigDecrypter.Decrypt(DRSD.sApplUserPwCrypt);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form frmHostOpen = new UCOSPasswordDecrypter.frmSelectUCOSHost(myUCSHostCfg);
            frmHostOpen.FormClosed += new FormClosedEventHandler(FormSelectUCOSHost_Closed);
            frmHostOpen.ShowDialog();
        }
    }
}
