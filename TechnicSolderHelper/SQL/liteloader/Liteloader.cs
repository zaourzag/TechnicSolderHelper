using Mono.Data.Sqlite;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TechnicSolderHelper.SQL.liteloader
{
    public class Liteloader
    {
        public Meta Meta { get; set; }

        public Dictionary<string, Versions> Versions { get; set; }
    }

    public class Meta
    {
        public string Description { get; set; }

        public string Authors { get; set; }

        public string Url { get; set; }
    }

    public class Versions
    {
        public Dictionary<string, Dictionary<string, VersionInfo>> Artefacts { get; set; }
    }

    public class VersionInfo
    {
        public string TweakClass { get; set; }

        public string File { get; set; }

        public string Version { get; set; }

        public string Md5 { get; set; }

        public string Timestamp { get; set; }
    }

    public class LiteloaderVersion
    {
        public string File { get; set; }

        public string Version { get; set; }

        public string Md5 { get; set; }

        public string McVersion { get; set; }

        public string TweakClass { get; set; }
    }

    public class LiteloaderSqlHelper : SqlHelper
    {
        protected readonly string CreateTableString;

        public LiteloaderSqlHelper()
            : base("liteloader")
        {
            CreateTableString = "CREATE TABLE IF NOT EXISTS 'liteloader' (file TEXT, version TEXT, md5 TEXT UNIQUE, mcversion TEXT, tweakClass TEXT, PRIMARY KEY(md5));";
            ExecuteDatabaseQuery(CreateTableString);
        }

        public void AddVersion(string file, string version, string md5, string mcversion, string tweakClass)
        {
            string sql = string.Format("INSERT OR REPLACE INTO {0} ('file', 'version', 'md5', 'mcversion', 'tweakClass') VALUES ('{1}','{2}','{3}','{4}','{5}');", TableName, file, version, md5, mcversion, tweakClass);
            Debug.WriteLine(sql);
            ExecuteDatabaseQuery(sql);
        }

        public LiteloaderVersion GetInfo(string md5)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE md5 LIKE '{1}';", TableName, md5);
            LiteloaderVersion llVersion = new LiteloaderVersion();
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
                                llVersion.Md5 = reader["md5"].ToString();
                                llVersion.McVersion = reader["mcversion"].ToString();
                                llVersion.Version = reader["version"].ToString();
                                llVersion.File = reader["file"].ToString();
                                llVersion.TweakClass = reader["tweakClass"].ToString();
                            }

                            return llVersion;
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
                            llVersion.Md5 = reader["md5"].ToString();
                            llVersion.McVersion = reader["mcversion"].ToString();
                            llVersion.Version = reader["version"].ToString();
                            llVersion.File = reader["file"].ToString();
                            llVersion.TweakClass = reader["tweakClass"].ToString();
                        }

                        return llVersion;
                    }
                }
            }
        }
    }
}

