using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.Cryptography;

namespace TechnicSolderHelper.AmazonS3
{
    public partial class S3Info : Form
    {
        private S3 _service;

        public S3Info()
        {
            InitializeComponent();
        }

        private void test_Click(object sender, EventArgs e)
        {
            if (!(serviceUrlTextBox.Text.StartsWith("http://") || serviceUrlTextBox.Text.StartsWith("https://")))
            {
                serviceUrlTextBox.Text = "http://" + serviceUrlTextBox.Text;
            }
            if (IsEveryFilledIn())
            {
                if (!Uri.IsWellFormedUriString(serviceUrlTextBox.Text, UriKind.Absolute))
                {
                    MessageBox.Show("Service url is not valid");
                    return;
                }

                _service = new S3(accessKeyTextBox.Text, secretKeyTextBox.Text, serviceUrlTextBox.Text);
                try
                {
                    _service.GetBucketList();
                    if (sender != null)
                    {
                        MessageBox.Show("Connection Succesful.");
                    }
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Connection not succesful.\n{0}", exception.Message));
                }
                GetBuckets();
            }
            else
            {
                MessageBox.Show("Please fill in everything.");
            }
        }

        private bool test_Click()
        {
            if (!(serviceUrlTextBox.Text.StartsWith("http://") || serviceUrlTextBox.Text.StartsWith("https://")))
            {
                serviceUrlTextBox.Text = "http://" + serviceUrlTextBox.Text;
            }
            if (IsEveryFilledIn())
            {
                if (!Uri.IsWellFormedUriString(serviceUrlTextBox.Text, UriKind.Absolute))
                {
                    MessageBox.Show("Service url is not valid");
                    return false;
                }

                _service = new S3(accessKeyTextBox.Text, secretKeyTextBox.Text, serviceUrlTextBox.Text);
                try
                {
                    _service.GetBucketList();
                    GetBuckets();
                    return true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Connection not succesful.\n{0}", exception.Message));
                }

            }
            else
            {
                MessageBox.Show("Please fill in everything.");
            }
            return false;
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void GetBuckets()
        {
            List<string> bucketList = _service.GetBucketList();
            bucketsListBox.Items.Clear();
            if (bucketList != null)
            {
                foreach (string bucket in bucketList)
                {
                    bucketsListBox.Items.Add(bucket);
                }
            }
        }

        private bool IsEveryFilledIn()
        {
            if (string.IsNullOrWhiteSpace(accessKeyTextBox.Text) || string.IsNullOrWhiteSpace(serviceUrlTextBox.Text) || string.IsNullOrWhiteSpace(secretKeyTextBox.Text))
            {
                return false;
            }
            return true;
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (IsEveryFilledIn())
            {
                if (bucketsListBox.SelectedItem != null)
                {
                    string bucket = bucketsListBox.SelectedItem.ToString();
                    ConfigHandler ch = new ConfigHandler();
                    Crypto crypto = new Crypto();
                    ch.SetConfig("S3url", serviceUrlTextBox.Text);
                    ch.SetConfig("S3accessKey", crypto.EncryptToString(accessKeyTextBox.Text));
                    ch.SetConfig("S3secretKey", crypto.EncryptToString(secretKeyTextBox.Text));
                    ch.SetConfig("S3Bucket", bucket);
                    Close();
                }
                else
                {
                    MessageBox.Show("Please select a bucket");
                }
            }
        }

        private void S3Info_Load(object sender, EventArgs e)
        {
            try
            {
                ConfigHandler ch = new ConfigHandler();
                Crypto crypto = new Crypto();
                serviceUrlTextBox.Text = ch.GetConfig("S3url");
                accessKeyTextBox.Text = crypto.DecryptString(ch.GetConfig("S3accessKey"));
                secretKeyTextBox.Text = crypto.DecryptString(ch.GetConfig("S3secretKey"));
            }
            catch
            {
                // ignored
            }
        }

        private void createNewBucketButton_Click(object sender, EventArgs e)
        {
            if (_service != null)
            {
                if (string.IsNullOrWhiteSpace(newBucketNameTextBox.Text))
                {
                    MessageBox.Show("You need to enter a name of the new bucket.");
                }
                else
                {
                    _service.CreateNewBucket(newBucketNameTextBox.Text);
                    test_Click();
                    bucketsListBox.SelectedItem = newBucketNameTextBox.Text;
                }
            }
            else
            {
                test_Click();
            }
        }
    }
}
