using System;
using System.Windows.Forms;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.Cryptography;

namespace TechnicSolderHelper.SQL
{
    public partial class SqlInfo : Form
    {
        public SqlInfo()
        {
            InitializeComponent();
            Crypto crypto = new Crypto();
            ConfigHandler ch = new ConfigHandler();
            databaseTextBox.Text = ch.GetConfig("mysqlDatabase");
            serverAddressTextBox.Text = ch.GetConfig("mysqlAddress");
            passwordTextBox.Text = crypto.DecryptString(ch.GetConfig("mysqlPassword"));
            usernameTextBox.Text = ch.GetConfig("mysqlUsername");
            prefixTextBox.Text = ch.GetConfig("mysqlPrefix");
        }

        private void testButton_Click(object sender, EventArgs e)
        {
            if (IsEverythingFilledIn())
            {
                SolderSqlHandler solderSqlHandler = new SolderSqlHandler(serverAddressTextBox.Text, usernameTextBox.Text, passwordTextBox.Text, databaseTextBox.Text, prefixTextBox.Text);
                solderSqlHandler.TestConnection();
            }
            else
            {
                MessageBox.Show("Please fill out all the data");
            }
        }

        private bool IsEverythingFilledIn()
        {
            return !string.IsNullOrWhiteSpace(databaseTextBox.Text) && !string.IsNullOrWhiteSpace(serverAddressTextBox.Text) && !string.IsNullOrWhiteSpace(usernameTextBox.Text);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (IsEverythingFilledIn())
            {
                ConfigHandler ch = new ConfigHandler();
                Crypto crypto = new Crypto();
                ch.SetConfig("mysqlUsername", usernameTextBox.Text);
                ch.SetConfig("mysqlPassword", crypto.EncryptToString(passwordTextBox.Text));
                ch.SetConfig("mysqlAddress", serverAddressTextBox.Text);
                ch.SetConfig("mysqlDatabase", databaseTextBox.Text);
                ch.SetConfig("mysqlPrefix", prefixTextBox.Text);
                Close();
            }
            else
            {
                MessageBox.Show("Please fill out all the data");
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }


    }
}
