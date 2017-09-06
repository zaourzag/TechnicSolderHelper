using System;
using System.IO;
using Renci.SshNet;
using TechnicSolderHelper.cryptography;
using TechnicSolderHelper.Confighandler;

namespace TechnicSolderHelper.FileUpload.sftp
{
    internal class SFTP
    {
        private readonly ConfigHandler cfg = new ConfigHandler();
        private readonly SftpClient client;

        public SFTP()
        {
            Crypto crypto = new Crypto();
            client = new SftpClient(cfg.GetConfig("sftpHost"), int.Parse(cfg.GetConfig("sftpPort")),
                cfg.GetConfig("sftpUserName"), crypto.DecryptString(cfg.GetConfig("sftpPassword")));
            client.Connect();
        }

        public SFTP(string host, string username,
            string password, int port)
        {
            client = new SftpClient(host, port, username, password);
            client.Connect();
        }


        public void UploadSFTPFile(string sourcefile, string destinationpath)
        {
            
            client.ChangeDirectory(destinationpath);
            using (var fs = new FileStream(sourcefile, FileMode.Open))
            {
                client.BufferSize = 4 * 1024;
                client.UploadFile(fs, Path.GetFileName(sourcefile));
            }
        }

        public void UploadFolder(string localPath, string remotePath)
        {
            Console.WriteLine("Uploading directory {0} to {1}", localPath, remotePath);

            var infos =
                new DirectoryInfo(localPath).EnumerateFileSystemInfos();
            foreach (var info in infos)
                if (info.Attributes.HasFlag(FileAttributes.Directory))
                {
                    var subPath = remotePath + "/" + info.Name;
                    if (!client.Exists(subPath))
                        client.CreateDirectory(subPath);
                    UploadFolder(info.FullName, remotePath + "/" + info.Name);
                }
                else
                {
                    using (Stream fileStream = new FileStream(info.FullName, FileMode.Open))
                    {
                        Console.WriteLine(
                            "Uploading {0} ({1:N0} bytes)", info.FullName, ((FileInfo) info).Length);
                        client.UploadFile(fileStream, remotePath + "/" + info.Name);
                    }
                }
        }

        public void Dispose()
        {
            client.Disconnect();
            client.Dispose();
        }
    }
}