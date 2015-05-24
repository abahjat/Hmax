using System;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Windows;
namespace Hmax
{
    public partial class SettingsForm : Form
    {
        private readonly ContextMenus cm;
        private bool certInUse = true;
        public SettingsForm(ContextMenus cmObj)
        {
            cm = cmObj;
            InitializeComponent();
            if (certInUse)
            {
                textBoxCert.Text = cm.CertSubject;
                statusLabel.Text = "Certificate in use";
                
            }
            else if (cm.Path.Length > 0)
            {
                statusLabel.Text = "File in Use: " + cm.Path;
            }
            else
            {
                statusLabel.Text = "Certificate in use";
            }

            textBoxChar.Text = cm.CharString;
          
        }

        private void button2_Click(object sender, EventArgs e)
        {
            cm.ResetCert(textBoxCert.Text);
            certInUse = true;
            statusLabel.Text = "Certificate in use";
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                cm.ResetCert(null);
                statusLabel.Text = "Smart Card in Use";
                certInUse = false;
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

        private void button4_Click(object sender, EventArgs e)
        {
            string hashstring="";


            
            try
            {
                OpenFileDialog openFileDialog1 = new OpenFileDialog();
                // Call the ShowDialog method to show the dialog box.
                DialogResult userClickedOK = openFileDialog1.ShowDialog();

                RIPEMD160 myRIPEMD160 = RIPEMD160Managed.Create();
                byte[] hashValue;

#if DEBUG
                Console.WriteLine(userClickedOK);
#endif
                // Process input if the user clicked OK.
                if (userClickedOK == DialogResult.OK)
                {
                    // Open the selected file to read.
                    FileStream fileStream = new FileStream(openFileDialog1.FileName, FileMode.Open);
                    fileStream.Position = 0;
                    // Compute the hash of the fileStream.
                    hashValue = myRIPEMD160.ComputeHash(fileStream);
                    

                    cm.SetFile(hashValue);
#if DEBUG
                    Console.WriteLine("hash=" + PrintByteArray(hashValue));
#endif
                    cm.Path = openFileDialog1.FileName;
                    fileStream.Close();
                }

                statusLabel.Text = "File in Use: " + openFileDialog1.FileName;
                certInUse = false;


            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine("Error: The directory specified could not be found.");
            }
            catch (IOException)
            {
                Console.WriteLine("Error: A file in the directory could not be accessed.");
            }
        }

        // Print the byte array in a readable format. 
        public string PrintByteArray(byte[] array)
        {
            int i;
            string hash = "";
            for (i = 0; i < array.Length; i++)
            {
                hash += String.Format("{0:X2}", array[i]);
                if ((i % 4) == 3) hash +=" ";
            }
            return hash;
        }
    }
}