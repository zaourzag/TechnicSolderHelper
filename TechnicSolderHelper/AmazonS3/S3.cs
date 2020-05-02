using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.Cryptography;

namespace TechnicSolderHelper.AmazonS3
{
    class S3
    {
        private readonly AmazonS3Client _client;
        private readonly string _bucket;

        public S3()
        {
            ConfigHandler ch = new ConfigHandler();
            Crypto crypto = new Crypto();
            var accessKey = crypto.DecryptString(ch.GetConfig("S3accessKey"));
            var secretKey = crypto.DecryptString(ch.GetConfig("S3secretKey"));
            string serviceUrl = ch.GetConfig("S3url");
            _bucket = ch.GetConfig("S3Bucket");
            var config = new AmazonS3Config() { ServiceURL = serviceUrl, Timeout = TimeSpan.FromSeconds(15), ReadWriteTimeout = TimeSpan.FromSeconds(15) };
            _client = AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, config) as AmazonS3Client;
        }

        public S3(string accessKey, string secretKey, string serviceUrl)
        {
            var config = new AmazonS3Config() { ServiceURL = serviceUrl, Timeout = TimeSpan.FromSeconds(15), ReadWriteTimeout = TimeSpan.FromSeconds(15) };
            _client = AWSClientFactory.CreateAmazonS3Client(accessKey, secretKey, config) as AmazonS3Client;
        }

        public List<string> GetBucketList()
        {
            try
            {

                ListBucketsResponse response = _client.ListBuckets();
                return response.Buckets.Select(bucket => bucket.BucketName).ToList();
            }
            catch
            {
                return null;
            }
        }

        public void CreateNewBucket(string bucketName)
        {
            try
            {
                PutBucketRequest request = new PutBucketRequest { BucketName = bucketName };
                _client.PutBucket(request);
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Could not create bucket:\n{0}", exception.Message));
            }
        }

        public void UploadFolder(string folderPath)
        {
            MessageToUser m = new MessageToUser();
            Thread startingThread = new Thread(m.UploadToS3);
            startingThread.Start();
            TransferUtilityUploadDirectoryRequest request = new TransferUtilityUploadDirectoryRequest()
            {
                BucketName = _bucket,
                Directory = folderPath,
                SearchOption = SearchOption.AllDirectories,
                SearchPattern = "*.zip",
                KeyPrefix = "mods"
            };
            TransferUtility directoryTransferUtility = new TransferUtility(_client);
            directoryTransferUtility.UploadDirectory(request);
            MessageBox.Show("Done uploading files to S3");
        }
    }
}
