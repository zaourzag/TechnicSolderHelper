using System;
using Mono.Data.Sqlite;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Net;

namespace TechnicSolderHelper.SQL.liteloader
{
    public sealed class LiteloaderSqlHelper : SqlHelper
    {
        public LiteloaderSqlHelper() : base("liteloader")
        {
            //reset the table if it contains unnecessary TweakClass info
            if (DoesFieldExist("tweakClass"))
            {
                ResetTable();
            }

            const string createTableString = "CREATE TABLE IF NOT EXISTS 'liteloader' (file TEXT, version TEXT, md5 TEXT UNIQUE, mcversion TEXT, PRIMARY KEY(md5));";
            ExecuteDatabaseQuery(createTableString);
        }

        public void AddVersion(string file, string version, string md5, string mcversion)
        {
            string sql = string.Format("INSERT OR REPLACE INTO {0} ('file', 'version', 'md5', 'mcversion') VALUES ('{1}','{2}','{3}','{4}');", TableName, file, version, md5, mcversion);
            Debug.WriteLine(sql);
            ExecuteDatabaseQuery(sql);
        }

        public LiteloaderVersionInfo GetInfo(string md5)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE md5 LIKE '{1}';", TableName, md5);
            LiteloaderVersionInfo llVersionInfo = new LiteloaderVersionInfo();
            if (IsUnix())
            {
                using (SqliteConnection db = new SqliteConnection(ConnectionString))
                {
                    db.Open();
                    using (SqliteCommand cmd = new SqliteCommand(sql, db))
                    {
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                llVersionInfo.Md5 = reader["md5"].ToString();
                                llVersionInfo.McVersion = reader["mcversion"].ToString();
                                llVersionInfo.Version = reader["version"].ToString();
                                llVersionInfo.File = reader["file"].ToString();
                            }

                            return llVersionInfo;
                        }
                    }
                }
            }
            using (SQLiteConnection db = new SQLiteConnection(ConnectionString))
            {
                db.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                {
                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            llVersionInfo.Md5 = reader["md5"].ToString();
                            llVersionInfo.McVersion = reader["mcversion"].ToString();
                            llVersionInfo.Version = reader["version"].ToString();
                            llVersionInfo.File = reader["file"].ToString();
                        }

                        return llVersionInfo;
                    }
                }
            }
        }

        public void FindAllLiteloaderVersions()
        {
            string json;
            string liteloaderJsonFile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            liteloaderJsonFile = Path.Combine(liteloaderJsonFile, "SolderHelper", "liteloader.json");
            WebClient webClient = new WebClient();
            Uri webFile = new Uri("http://dl.liteloader.com/versions/versions.json");
            webClient.DownloadFile(webFile, liteloaderJsonFile);

            LiteloaderJson liteLoader;
            using (StreamReader streamReader = new StreamReader(liteloaderJsonFile))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();
                liteLoader = serializer.Deserialize<LiteloaderJson>(jsonReader);
            }

            //LiteloaderJson liteLoader2 = JsonConvert.DeserializeObject<LiteloaderJson>(json);

            foreach (KeyValuePair<string, Versions> item in liteLoader.Versions)
            {
                if (item.Value.Artefacts != null)// && item.Value.Artefacts.Count > 0)
                {
                    foreach (LiteloaderVersionInfo llVersionInfo in item.Value.Artefacts.LiteloaderVersions.Values)
                    {
                        AddVersion(llVersionInfo.File, llVersionInfo.Version, llVersionInfo.Md5,
                            item.Key);
                    }
                }
                //only look for snapshots if there were no artefacts (full releases) for the MC version, as snapshots could be less stable
                else if (item.Value.Snapshots != null)// && item.Value.Snapshots.Count > 0)
                {
                    foreach (LiteloaderVersionInfo llVersionInfo in item.Value.Snapshots.LiteloaderVersions.Values)
                    {
                        AddVersion(llVersionInfo.File, llVersionInfo.Version, llVersionInfo.Md5,
                            item.Key);
                    }
                }
            }
        }
    }
}

