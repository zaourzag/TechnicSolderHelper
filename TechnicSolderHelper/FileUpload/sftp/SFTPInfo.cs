using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TechnicSolderHelper.cryptography;
using TechnicSolderHelper.Confighandler;

namespace TechnicSolderHelper.FileUpload.sftp
{
    public partial class SFTPInfo : Form
    {
        public SFTPInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(user.Text) || String.IsNullOrWhiteSpace(password.Text) || String.IsNullOrWhiteSpace(host.Text))
            {
                MessageBox.Show("Please fill out all values");
            }
            else
            {
                
                    Crypto crypto = new Crypto();
                    ConfigHandler ch = new ConfigHandler();
                    ch.SetConfig("sftpUserName", user.Text);
                    ch.SetConfig("sftpHost", host.Text);
                ch.SetConfig("sftpPort", port.Text);
                ch.SetConfig("sftpPassword", crypto.EncryptToString(password.Text));
                ch.SetConfig("sftpDest", destination.Text);
                Close();
                }
                
            }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                SFTP sftp = new SFTP(host.Text, user.Text, password.Text, int.Parse(port.Text));
                sftp.UploadSFTPFile(
                    Path.Combine(Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location),
                        "TestFile.txt"), destination.Text );
                sftp.Dispose();
                MessageBox.Show("Uploaded Test File \"TestFile.txt\"", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

