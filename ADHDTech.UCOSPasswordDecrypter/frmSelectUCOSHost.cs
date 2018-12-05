using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UCOSPasswordDecrypter
{
    public partial class frmSelectUCOSHost : Form
    {
        WindowsFormsApplication1.UCOSHostCfg thisHostCfg;

        public frmSelectUCOSHost(WindowsFormsApplication1.UCOSHostCfg passedHostCfg)
        {
            InitializeComponent();
            thisHostCfg = passedHostCfg;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblUCOSHost_Click(object sender, EventArgs e)
        {

        }

        private void lblRemotePassphrase_Click(object sender, EventArgs e)
        {

        }

        private void frmSelectUCOSHost_Load(object sender, EventArgs e)
        {

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            thisHostCfg.sUCOSHost = tbUCOSHost.Text;
            thisHostCfg.sUCOSRemoteUser = tbRemoteUser.Text;
            thisHostCfg.sUCOSPassphrase = tbRemotePassphrase.Text;
            this.Close();
        }
    }
}
