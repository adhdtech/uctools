using ADHDTech.UCOSClients;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using System.Web.Script.Serialization;

namespace CUCM_AXL_Query
{
    public partial class MainForm : Form
    {
        public AXLSQLApp myAXLSQLApp;
        public TreeView profileTreeView;

        public MainForm()
        {
            InitializeComponent();
            myAXLSQLApp = new AXLSQLApp(this);
            profileTreeView = tvHosts;
            // Load config from file
            myAXLSQLApp.LoadConfig();
            RefreshServerProfiles();
            //GetTables();
        }

        private void toolStripMenuItemOpen_Click(object sender, EventArgs e)
        {
        }
        /*
        private void button1_Click(object sender, EventArgs e)
        {
            RunQueryToPane(myAXLSQLApp.currentAXLProfile, tbSQLQuery.Text);
        }
        */
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void GetTables(CUCMAXLProfile myAXLProfile)
        {
            // Clear table list
            tvTables.Nodes.Clear();

            // Run AXL Query
            CUCMAXLClient myClient = new CUCMAXLClient(myAXLProfile.AXLHost, myAXLProfile.AXLUser, myAXLProfile.AXLPass);
            if (myClient.RunQuery(@"select tabname, nrows  from systables where flags = 8 order by tabname asc"))
            {
                // We received an error; put the error in the status bar
                toolStripStatusLabel1.Text = "SQL Error: " + myClient.ErrorMsg;
                return;
            }

            // Set the record count in the status bar
            toolStripStatusLabel1.Text = "Table count: " + myClient.ReturnDataSet.Count.ToString();

            // Loop over all records
            foreach (Dictionary<string, string> sqlRecord in myClient.ReturnDataSet)
            {
                // Add to TreeView
                TreeNode rootNode = new TreeNode();
                Decimal rowCount = Decimal.Parse(sqlRecord["nrows"]);
                rootNode.Text = sqlRecord["tabname"] + " (" + rowCount.ToString("0.#") + ")";
                rootNode.Tag = sqlRecord["tabname"];
                tvTables.Nodes.Add(rootNode);
            }
        }

        private void RunTestRISPort(CUCMAXLProfile myAXLProfile)
        {
            RISService myRISClient = new RISService(myAXLProfile.AXLHost, myAXLProfile.AXLUser, myAXLProfile.AXLPass);
            CmSelectionCriteria filtre = new CmSelectionCriteria
            {
                SelectBy = "Name",
                MaxReturnedDevices = 20,
                Class = DeviceClass.Phone.ToString(),
                Model = 255 //"Any" Model.
            };

            //String stateInfo = null; // Execution summary

            //SelectCmDeviceResult result = myRISClient.SelectCmDevice(ref stateInfo, filtre);

            //CmNode[] nodes = result.CmNodes;

        }

        private void tvTables_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //toolStripStatusLabel1.Text = "Clicked: " + e.Node.Tag;
            //RunQueryToPane("select first 10 * from " + e.Node.Tag);
        }

        private void RunQueryToPane(CUCMAXLProfile myAXLProfile, string sqlQuery)
        {
            // Clear existing results
            dataGridView1.DataSource = null;
            dataGridView1.Rows.Clear();
            if (myAXLSQLApp.currentAXLProfile != null) {
                // Run AXL Query
                CUCMAXLClient myClient = new CUCMAXLClient(
                    myAXLProfile.AXLHost,
                    myAXLProfile.AXLUser,
                    myAXLProfile.AXLPass
                );
                if (myClient.RunQuery(sqlQuery))
                {

                    // We received an error; put the error in the status bar
                    toolStripStatusLabel1.Text = "SQL Error: " + myClient.ErrorMsg;
                    return;
                }

                // Set the record count in the status bar
                toolStripStatusLabel1.Text = "Record count: " + myClient.ReturnDataSet.Count.ToString();

                // If we received no results, don't bother populating the data table
                if (myClient.ReturnDataSet.Count == 0)
                {
                    return;
                }

                // Create a new data table
                DataTable sqlResultsTable = new DataTable("sqlResults");

                List<string> ColumnList = new List<string>();

                // Loop over the first record fields to get key names
                foreach (KeyValuePair<string, string> fieldPair in myClient.ReturnDataSet.First())
                {
                    ColumnList.Add(fieldPair.Key);
                    sqlResultsTable.Columns.Add(new DataColumn(fieldPair.Key));
                }

                // Loop over all records
                foreach (Dictionary<string, string> sqlRecord in myClient.ReturnDataSet)
                {
                    // Create row
                    DataRow newRow;
                    newRow = sqlResultsTable.NewRow();

                    // Loop over columns
                    foreach (string colName in ColumnList)
                    {
                        if (sqlRecord[colName] == null)
                        {
                            newRow[colName] = "<null>";
                        }
                        else
                        {
                            newRow[colName] = sqlRecord[colName];
                        }
                    }

                    // Add row
                    sqlResultsTable.Rows.Add(newRow);
                }

                dataGridView1.DataSource = sqlResultsTable;
            }
        }

        public void RefreshServerProfiles()
        {
            // Run refresh in form (should move this logic elsewhere)
            profileTreeView.Nodes.Clear();

            // Loop over profiles
            foreach (KeyValuePair<string, CUCMAXLProfile> entry in myAXLSQLApp.AXLProfiles)
            {
                // Add to TreeView
                TreeNode rootNode = new TreeNode();
                rootNode.Text = entry.Key;
                rootNode.Tag = entry.Key;
                myAXLSQLApp.mainForm.profileTreeView.Nodes.Add(rootNode);
            }
        }

        private void tvTables_AfterSelect(object sender, TreeViewEventArgs e)
        {
            RunQueryToPane(myAXLSQLApp.currentAXLProfile, "select first 10 * from " + e.Node.Tag);
        }

        private void tvHosts_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // Do this after selecting an item
            if (e.Node != null)
            {
                myAXLSQLApp.currentAXLProfile = myAXLSQLApp.AXLProfiles[(string)e.Node.Tag];
                GetTables(myAXLSQLApp.currentAXLProfile);
                RunTestRISPort(myAXLSQLApp.currentAXLProfile);
            }
        }

        private void tvHosts_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                // Display context menu for eg:
                contextMenuHosts.Show(this, new Point(e.X, e.Y + contextMenuHosts.Height));
            }
        }

        private void newServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show add server box
            Form serverConfig = new ServerProfileForm(myAXLSQLApp);
            serverConfig.Show();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            RunQueryToPane(myAXLSQLApp.currentAXLProfile, tbSQLQuery.Text);
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
            label1.Text = "CUCM AXL SQL Utility";
            label2.Text = "By: Pete Brown (jpbrown@adhdtech.com)";
            label3.Text = "Version 0.1\nBuild 20180327";
            aboutbox.Controls.Add(label1);
            aboutbox.Controls.Add(label2);
            aboutbox.Controls.Add(label3);
            label1.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 25);
            label2.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 50);
            label3.Location = new Point(aboutbox.Left + 25, aboutbox.Top + 75);
            aboutbox.ShowDialog();
        }

        private void tbSQLQuery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RunQueryToPane(myAXLSQLApp.currentAXLProfile, tbSQLQuery.Text);
            }
        }
    }
}
