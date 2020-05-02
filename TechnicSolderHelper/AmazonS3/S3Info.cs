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

        private bool testButton_Click(object sender = null, EventArgs e = null)
        {
            FillEmptyUrl();
            if (!(serviceUrlTextBox.Text.StartsWith("http://") || serviceUrlTextBox.Text.StartsWith("https://")))
            {
                serviceUrlTextBox.Text = "http://" + serviceUrlTextBox.Text;
            }
            if (IsEveryFilledIn())
            {
                if (!Uri.IsWellFormedUriString(serviceUrlTextBox.Text, UriKind.Absolute))
                {
                    MessageBox.Show("Service URL is not valid");
                    return false;
                }

                _service = new S3(accessKeyTextBox.Text, secretKeyTextBox.Text, serviceUrlTextBox.Text);
                try
                {
                    _service.GetBucketList();
                    GetBuckets();
                    if (sender != null)
                    {
                        MessageBox.Show("Connection successful.");
                    }

                    return true;
                }
                catch (Exception exception)
                {
                    MessageBox.Show(string.Format("Connection not successful.\n{0}", exception.Message));
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

        private void FillEmptyUrl()
        {
            if (string.IsNullOrWhiteSpace(serviceUrlTextBox.Text))
            {
                serviceUrlTextBox.Text = @"http://s3.amazonaws.com/";
            }
        }

        private bool IsEveryFilledIn()
        {
            FillEmptyUrl();
            return !(string.IsNullOrWhiteSpace(accessKeyTextBox.Text) ||
                     string.IsNullOrWhiteSpace(serviceUrlTextBox.Text) ||
                     string.IsNullOrWhiteSpace(secretKeyTextBox.Text));
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
                    MessageBox.Show("You need to enter a name for the new bucket.");
                }
                else
                {
                    _service.CreateNewBucket(newBucketNameTextBox.Text);
                    testButton_Click();
                    bucketsListBox.SelectedItem = newBucketNameTextBox.Text;
                }
            }
            else
            {
                testButton_Click();
            }
        }
    }
}
