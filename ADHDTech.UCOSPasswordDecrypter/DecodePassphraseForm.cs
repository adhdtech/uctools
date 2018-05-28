using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ADHDTech.CiscoCrypt;

namespace ADHDTech.CiscoCrypt
{
    public partial class DecodePassphraseForm : Form
    {
        public DecodePassphraseForm()
        {
            InitializeComponent();
            this.ActiveControl = tbRemoteSupportPassphrase;
        }

        private void tbRemoteSupportPassphrase_TextChanged(object sender, EventArgs e)
        {
            // Decode
            tbDecodedPassword.Text = new ADHDTech.CiscoCrypt.RemoteSupportPassphrase().Decode(tbRemoteSupportPassphrase.Text);
        }

        private void DecodePassphraseForm_Load(object sender, EventArgs e)
        {

        }
    }
}
