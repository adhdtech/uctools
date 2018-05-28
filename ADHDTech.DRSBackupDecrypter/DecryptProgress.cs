using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using DRSBackupDecrypter;
using ADHDTech.CiscoCrypt;

namespace DRSBackupDecrypter
{
    public partial class DecryptProgress : Form
    {
        public DecryptProgress()
        {
            InitializeComponent();
        }

        private void progressBar2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {

        }

        private void Worker_DoWork(object sender, EventArgs e)
        {
            DRSD.bTermEarly = false;
            DRSD.iDecryptFilesProcessed = 0;
            BackgroundWorker worker = sender as BackgroundWorker;
            for (int i = 0; i < DRSD.iDecryptFileCount; i++)
            {
                DRSD.sFilenameEncrypted = DRSD.sBackupSetDirectory + "\\" + DRSD.sFilesToDecrypt[i];
                DRSD.sFilenameDecryptTo = DRSD.sOutputDirectory + "\\" + DRSD.sFilesToDecrypt[i];

                TARFileObj thisTarObj = DRSD.myBackupSet._dTARFiles[DRSD.sFilesToDecrypt[i]];
                DRSD.lCurrentFileSize = thisTarObj._lFileSize;

                thisTarObj.Decrypt(DRSD.sFilenameDecryptTo, DRSD.myBackupSet._sRandomBackupPass, DRSD.myBackupSet._iHashTypeTAR, worker);

                if (thisTarObj._bTermEarly)
                {
                    break;
                }
                else {
                    DRSD.iDecryptFilesProcessed++;
                    worker.ReportProgress(100);
                }
        }

            //label7.Text = (i + 1).ToString() + "/" + DRSD.iDecryptFileCount.ToString();
            //    progressBar2.Value = ((100 * (i + 1)) / DRSD.iDecryptFileCount);
        }
        //label4.Visible = true;
        

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void Form3_Shown(object sender, EventArgs e)
        {
            label4.Visible = false;
            DRSD.iDecryptFileCount = DRSD.sFilesToDecrypt.Count();
            label7.Text = "0/" + DRSD.iDecryptFileCount.ToString();
            //this.Refresh();

            BackgroundWorker worker = new BackgroundWorker
            {
                WorkerReportsProgress = true
            };
            worker.DoWork += new DoWorkEventHandler(Worker_DoWork);
            worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
            worker.RunWorkerAsync();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            progressBar2.Value = (DRSD.iDecryptFilesProcessed * 100) / DRSD.iDecryptFileCount;
            label5.Text = Path.GetFileName(DRSD.sFilenameEncrypted);
            label6.Text = ((DRSD.lCurrentFileSize * e.ProgressPercentage * .01) / 1024 / 1024).ToString("0") + " MB / " + (DRSD.lCurrentFileSize / 1024 / 1024).ToString() + " MB";
            label7.Text = DRSD.iDecryptFilesProcessed.ToString() + " / " + DRSD.iDecryptFileCount.ToString();

            if (DRSD.iDecryptFilesProcessed == DRSD.iDecryptFileCount)
            {
                label4.Visible = true;
                MessageBox.Show("Decryption complete!");
                this.Close();
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void KillWorkerThread(object sender, FormClosingEventArgs e)
        {
            DRSD.bTermEarly = true;
        }
    }
}
