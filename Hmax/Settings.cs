using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hmax
{
    public partial class SettingsForm : Form
    {
        private ContextMenus cm;
        public SettingsForm(ContextMenus cmObj)
        {
            cm = cmObj;
            InitializeComponent();
            textBoxCert.Text = cm.CertSubject;
            textBoxChar.Text = cm.CharString;
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cm.ResetCert(textBoxCert.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cm.ResetCert(null);
            }
            catch (Exception)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            cm.CharString = textBoxChar.Text;
        }
    }
}
