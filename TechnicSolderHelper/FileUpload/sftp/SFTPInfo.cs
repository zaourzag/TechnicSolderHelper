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
        ConfigHandler ch = new ConfigHandler();
        Crypto crypto = new Crypto();
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
            string lines = "First line.\r\nSecond line.\r\nThird line.\r\nUpload OK!";

            // Write the string to a file.
            Guid g = Guid.NewGuid();
            string uuid = Convert.ToBase64String(g.ToByteArray());
            uuid = uuid.Replace("=", "");
            string filePath = (System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TestFile-" + uuid + ".txt"));
            System.IO.StreamWriter file = new System.IO.StreamWriter(filePath);
            file.WriteLine(lines);

            file.Close();
            try
            {
                
                SFTP sftp = new SFTP(host.Text, user.Text, password.Text, int.Parse(port.Text));
                sftp.UploadSFTPFile(
                    filePath, destination.Text );
                sftp.Delete(filePath);
                sftp.Dispose();
                MessageBox.Show("Uploaded Test File \"TestFile.txt\"", "OK", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } finally { File.Delete(filePath);}
        }

        private void SFTPInfo_Load(object sender, EventArgs e)
        {
            try
            {
                user.Text = ch.GetConfig("sftpUserName");
                host.Text = ch.GetConfig("sftpHost");
                port.Text = ch.GetConfig("sftpPort");
                password.Text = crypto.DecryptString(ch.GetConfig("sftpPassword"));
                destination.Text = ch.GetConfig("sftpDest");
            }
            catch (Exception)
            {
                
            }
        }
    }
}

