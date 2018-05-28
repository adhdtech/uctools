using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CUCM_AXL_Query
{
    public partial class ServerProfileForm : Form
    {
        AXLSQLApp myAXLSQLApp;
        CUCMAXLProfile testProfile;
        public ServerProfileForm(AXLSQLApp passedAXLSQLApp)
        {
            InitializeComponent();
            myAXLSQLApp = passedAXLSQLApp;
        }

        private void ServerProfileForm_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            testProfile = new CUCMAXLProfile(tb_AXLHost.Text, tb_AXLUser.Text, tb_AXLPass.Text);
            if (testProfile.Validated)
            {
                MessageBox.Show("Connection successful!");
            }
            else {
                MessageBox.Show("Connection failed with error: " + testProfile.AXLError);
            }
        }

        private void btn_AXLProfileSubmit_Click(object sender, EventArgs e)
        {
            if (testProfile != null && testProfile.Validated) {
                myAXLSQLApp.AXLProfiles[testProfile.AXLUser + @"@" + testProfile.AXLHost] = testProfile;

                // Save config
                myAXLSQLApp.SaveConfig();
                myAXLSQLApp.mainForm.RefreshServerProfiles();

                Close();
            } else {
                MessageBox.Show("Connection has not been validated");
            }
        }

        private void tb_AXLHost_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
