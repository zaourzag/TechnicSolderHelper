using System;
using System.Net;
using System.Windows.Forms;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.Cryptography;

namespace TechnicSolderHelper.FileUpload
{
    public partial class FtpInfo : Form
    {
        public FtpInfo()
        {
            InitializeComponent();
            string url = "";
            string username = "";
            string pass = "";
            Crypto crypto = new Crypto();
            try
            {
                ConfigHandler ch = new ConfigHandler();
                url = ch.GetConfig("ftpUrl");
                username = ch.GetConfig("ftpUserName");
                pass = crypto.DecryptString(ch.GetConfig("ftpPassword"));
            }
            catch (Exception)
            {
                // ignored
            }
            usernameTextBox.Text = username;
            passwordTextBox.Text = pass;
            hostTextBox.Text = url;
        }

        private void acceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(usernameTextBox.Text) || string.IsNullOrWhiteSpace(passwordTextBox.Text) || string.IsNullOrWhiteSpace(hostTextBox.Text))
            {
                MessageBox.Show("Please fill out all values");
            }
            else
            {
                string url = hostTextBox.Text;
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    Crypto crypto = new Crypto();
                    ConfigHandler ch = new ConfigHandler();
                    ch.SetConfig("ftpUserName", usernameTextBox.Text);
                    ch.SetConfig("ftpUrl", url);
                    ch.SetConfig("ftpPassword", crypto.EncryptToString(passwordTextBox.Text));
                    Close();
                }
                else
                {
                    MessageBox.Show("Hostname is not valid");
                }

            }
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            string url = hostTextBox.Text;
            string name = usernameTextBox.Text;
            string pass = passwordTextBox.Text;

            MessageBox.Show(IsValidConnection(url, name, pass));
        }

        private string IsValidConnection(string url, string user, string password)
        {
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                try
                {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    request.Credentials = new NetworkCredential(user, password);
                    request.GetResponse();

                }
                catch (WebException ex)
                {
                    return ex.Message + " --> " + ((FtpWebResponse)ex.Response).StatusDescription; ;
                }
                return "All is working fine!!";
            }
            return "Invalid hostname";
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void hostTextBox_TextChanged(object sender, EventArgs e)
        {
            if (!hostTextBox.Text.StartsWith("ftp://"))
            {
                hostTextBox.Text = "ftp://" + hostTextBox.Text;
            }
        }
    }
}
