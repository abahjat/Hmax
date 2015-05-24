using System;
using System.Windows.Forms;

namespace Hmax
{
    public partial class SettingsForm : Form
    {
        private readonly ContextMenus cm;

        public SettingsForm(ContextMenus cmObj)
        {
            cm = cmObj;
            InitializeComponent();
            textBoxCert.Text = cm.CertSubject;
            textBoxChar.Text = cm.CharString;
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

        private void textBox1_Enter(object sender, EventArgs e)
        {
            textBox1.Text = "";
        }
    }
}