using System;
using System.IO;
using System.Windows.Forms;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.Cryptography;

namespace TechnicSolderHelper.FileUpload.Sftp
{
    public partial class SftpInfo : Form
    {
        ConfigHandler ch = new ConfigHandler();
        Crypto crypto = new Crypto();
        public SftpInfo()
        {
            InitializeComponent();
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(userTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text) || string.IsNullOrWhiteSpace(hostTextBox.Text))
            {
                MessageBox.Show("Please fill out all values");
            }
            else
            {
                ch.SetConfig("sftpUserName", userTextBox.Text);
                ch.SetConfig("sftpHost", hostTextBox.Text);
                ch.SetConfig("sftpPort", portInput.Text);
                ch.SetConfig("sftpPassword", crypto.EncryptToString(passwordTextBox.Text));
                ch.SetConfig("sftpDest", destinationTextBox.Text);
                Close();
            }

        }

        private void uploadTestFileButton_Click(object sender, EventArgs e)
        {
            string lines = "First line.\r\nSecond line.\r\nThird line.\r\nUpload OK!";

            // Write the string to a file.
            string uuid = Guid.NewGuid().ToString();

            string filePath = (System.IO.Path.Combine(System.IO.Path.GetTempPath(), "TestFile-" + uuid + ".txt"));
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(filePath))
            {
                file.WriteLine(lines);

                file.Close();
                try
                {
                    Sftp sftp = new Sftp(hostTextBox.Text, userTextBox.Text, passwordTextBox.Text, int.Parse(portInput.Text));
                    sftp.UploadSFTPFile(
                        filePath, destinationTextBox.Text);
                    sftp.Delete(filePath);
                    sftp.Dispose();
                    MessageBox.Show("Uploaded Test File (and already deleted it.)", "OK", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    File.Delete(filePath);
                }
            }
        }

        private void SftpInfo_Load(object sender, EventArgs e)
        {
            try
            {
                userTextBox.Text = ch.GetConfig("sftpUserName");
                hostTextBox.Text = ch.GetConfig("sftpHost");
                portInput.Text = ch.GetConfig("sftpPort");
                passwordTextBox.Text = crypto.DecryptString(ch.GetConfig("sftpPassword"));
                destinationTextBox.Text = ch.GetConfig("sftpDest");
            }
            catch (Exception)
            {
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}

