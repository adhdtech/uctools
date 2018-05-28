using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using ADHDTech.Informix;

namespace IFX_Ontape_Viewer
{
    public partial class Form1 : Form
    {
        OntapeReader MyTapeReader;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Spring = true;
            toolStripStatusLabel1.Text = "";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectOntapeFile();
        }

        public void SelectOntapeFile()
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                MyTapeReader = new OntapeReader();
                tvDBList.Nodes.Clear();
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                toolStripProgressBar1.Visible = true;
                BackgroundWorker worker = new BackgroundWorker();
                worker.WorkerReportsProgress = true;
                worker.DoWork += new DoWorkEventHandler(LoadOntapeFile);
                worker.ProgressChanged += new ProgressChangedEventHandler(worker_ProgressChanged);
                worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
                worker.RunWorkerAsync();
            }
        }

        public void LoadOntapeFile(object sender, EventArgs e)
        {
            //BackgroundWorker worker = sender as BackgroundWorker;
            MyTapeReader.OntapeWorker = sender as BackgroundWorker;
            MyTapeReader.OntapeFile = openFileDialog1.FileName;
            MyTapeReader.LoadBackup();
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolStripProgressBar1.Value = e.ProgressPercentage;
        }

        public void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            foreach (OntapeReader.DBSpace dbProfile in MyTapeReader._DBSpaces)
            {
                if (dbProfile.Name != null)
                {
                    TreeNode tvDBItem = new TreeNode(dbProfile.Name);
                    //foreach (ADHDTech.OntapeReader.DBTableDetails thisTable in dbProfile.Tables)
                    foreach (KeyValuePair<String, OntapeReader.DBTableDetails> keyValPair in dbProfile.TableDictionary)
                    {
                        OntapeReader.DBTableDetails thisTable = keyValPair.Value;
                        if ((thisTable.TableType == 0x0A || thisTable.TableType == 0x02) && thisTable.NumCols > 0)
                        {
                            TreeNode tvTableItem = new TreeNode(thisTable.TableName);
                            tvTableItem.Tag = "Table";
                            // Add Columns to table
                            if (thisTable.DBColumns != null)
                            {
                                foreach (OntapeReader.DBColumnRecord thisCol in thisTable.DBColumns)
                                {
                                    string colName = "<MISSING>";
                                    if (thisCol.colname != null)
                                    {
                                        colName = thisCol.colname + '(' + thisCol.coldetails.coltype.ToString("X") + "|" + thisCol.coldetails.collength.ToString("X") + ')';
                                    }
                                    TreeNode tvColItem = new TreeNode(colName);
                                    tvTableItem.Nodes.Add(tvColItem);
                                }
                            }
                            tvDBItem.Nodes.Add(tvTableItem);
                        }
                    }
                    tvDBList.Nodes.Add(tvDBItem);
                }
            }
            toolStripProgressBar1.Value = 0;
            toolStripProgressBar1.Visible = false;
        }

        public void ProcessRow(OntapeReader.DBValue[] rowData)
        {
            int dvRecIndex = dataGridView1.Rows.Add();
            DataGridViewRow thisRecord = dataGridView1.Rows[dvRecIndex];

            for (int k = 0; k < rowData.Count(); k++) {
                OntapeReader.DBValue thisValue = rowData[k];
                string ColDataValue = "";
                // Output column data
                if (thisValue.dataLen > 0 && thisValue.isNull == 0)
                {
                    switch (thisValue.dataType)
                    {
                        case 0x0000:    // TEXT
                            if (thisValue.dataVal[0] != 0x00)
                            {
                                ColDataValue = System.Text.Encoding.UTF8.GetString(thisValue.dataVal);
                            }
                            else
                            {
                                ColDataValue = "<EMPTY>";
                            }
                            break;
                        case 0x0001:    // SMALLINT
                            //thisValue.dataVal.Reverse();
                            ColDataValue = "0x" + (OntapeReader.ReverseInt16((ushort)BitConverter.ToInt16(thisValue.dataVal, 0))).ToString("X4");
                            break;
                        case 0x0002:    // INT
                            //thisValue.dataVal.Reverse();
                            ColDataValue = "0x" + (OntapeReader.ReverseInt32((uint)BitConverter.ToInt32(thisValue.dataVal, 0))).ToString("X8");
                            break;
                        case 0x0003:    // FLOAT
                            break;
                        case 0x0009:    // NULL
                            break;
                        case 0x000A:    // DATETIME
                            break;
                        case 0x0029:    // BOOL
                            ColDataValue = (BitConverter.ToBoolean(thisValue.dataVal, 0)).ToString();
                            break;
                        case 0x0034:    // LONG
                            //thisValue.dataVal.Reverse();
                            ColDataValue = "0x" + (OntapeReader.ReverseInt64((ulong)BitConverter.ToInt64(thisValue.dataVal, 0))).ToString("X16");
                            break;
                        case 0x1029:    // BINARY FILE POINTER DATA
                            ColDataValue = (BitConverter.ToString(thisValue.dataVal)).Replace("-","");
                            break;
                    }
                }
    
                if (String.IsNullOrEmpty(ColDataValue))
                {
                    ColDataValue = "<NULL>";
                }

                thisRecord.Cells[dataGridView1.Columns[k].Name].Value = ColDataValue;
            }
        }

        private unsafe void tvDBList_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = tvDBList.SelectedNode;
            if ((string)node.Tag == "Table") {
                //MessageBox.Show(string.Format("You selected table: {0}", node.Text));
                dataGridView1.Columns.Clear();

                OntapeReader.DBTableDetails thisTable = new OntapeReader.DBTableDetails();

                for (int i = 0; i < MyTapeReader._DBSpaces.Count(); i++)
                {
                    // Loop over Tables - look for all "systables"
                    for (int j = 0; j < MyTapeReader._DBSpaces[i].TableCount; j++)
                    {
                        //string wantedTable = "devicenumplanmap";
                        if (MyTapeReader._DBSpaces[i].Tables[j].TableName == node.Text)
                        {
                            thisTable = MyTapeReader._DBSpaces[i].Tables[j];
                        }
                    }
                }

                if (thisTable.TableName != "") {
                    for (int i = 0; i < thisTable.NumCols; i++)
                    {
                        string newColName = thisTable.DBColumns[i].colname;
                        DataGridViewTextBoxColumn newCol = new DataGridViewTextBoxColumn();
                        newCol.DataPropertyName = newColName;
                        newCol.HeaderText = newColName;
                        newCol.Name = newColName;
                        dataGridView1.Columns.Add(newCol);
                    }

                    // Callback
                    OntapeReader.DBTableDetails.RecHandlerCallBack recHandler = new OntapeReader.DBTableDetails.RecHandlerCallBack(ProcessRow);
                    thisTable.GetRecords(10, 0, recHandler);
                    if (dataGridView1.SelectedCells.Count > 0)
                        dataGridView1.ContextMenuStrip = contextMenuStrip1;
                } // End if found table
            }
        }

        private void tvDBList_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void copyDataGridVal_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        private void CopyToClipboard()
        {
            //Copy to clipboard
            DataObject dataObj = dataGridView1.GetClipboardContent();
            if (dataObj != null)
                Clipboard.SetDataObject(dataObj);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            label1.Text = "IFX Ontape Browser";
            label2.Text = "By: Pete Brown (jpbrown@adhdtech.com)";
            label3.Text = "Test Build 20150413";
            aboutbox.Controls.Add(label1);
            aboutbox.Controls.Add(label2);
            aboutbox.Controls.Add(label3);
            label1.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 25);
            label2.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 50);
            label3.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 75);
            aboutbox.ShowDialog();
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripProgressBar1_Click_1(object sender, EventArgs e)
        {

        }
    }
}
