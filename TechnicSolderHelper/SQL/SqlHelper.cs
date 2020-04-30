using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Security.Cryptography;
using System.Threading;

namespace TechnicSolderHelper.SQL
{
    public abstract class SqlHelper
    {
        protected SqlHelper(string tableName)
        {
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper"));
            string databaseName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "SolderHelper", "SolderHelper.db");
            try
            {
                if (!File.Exists(databaseName))
                {
                    if (IsUnix())
                    {
                        SqliteConnection.CreateFile(databaseName);
                    }
                    else
                    {
                        SQLiteConnection.CreateFile(databaseName);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            if (IsUnix())
            {
                SqliteConnectionStringBuilder c = new SqliteConnectionStringBuilder { DataSource = databaseName };
                ConnectionString = c.ConnectionString;
            }
            else
            {
                SQLiteConnectionStringBuilder c = new SQLiteConnectionStringBuilder { DataSource = databaseName };
                ConnectionString = c.ConnectionString;
            }
            TableName = tableName;
        }
        protected readonly string TableName;
        protected readonly string ConnectionString;

        protected static bool IsUnix()
        {
            return Environment.OSVersion.ToString().ToLower().Contains("unix");
        }

        protected void ExecuteDatabaseQuery(string sql, bool async = false)
        {
            if (IsUnix())
            {
                try
                {
                    using (SqliteConnection db = new SqliteConnection(ConnectionString))
                    {
                        db.Open();
                        using (SqliteCommand cmd = new SqliteCommand(sql, db))
                        {
                            if (async)
                            {
                                cmd.ExecuteNonQueryAsync();
                            }
                            else
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            else
            {
                /*try
                {*/
                    using (SQLiteConnection db = new SQLiteConnection(ConnectionString))
                    {
                        db.Open();
                        using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                        {
                            if (async)
                            {
                                cmd.ExecuteNonQueryAsync();
                            }
                            else
                            {
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }
                /*}
                catch (Exception)
                {
                    // ignored
                }*/
            }
        }

        public virtual void ResetTable()
        {
            string sql = string.Format("DROP TABLE {0};", TableName);
            ExecuteDatabaseQuery(sql);
        }

        private static Dictionary<string, string> md5Cache = new Dictionary<string, string>();

        public static string CalculateMd5(string file)
        {
            if (md5Cache.ContainsKey(file))
            {
                return md5Cache[file];
            }
            using (var md5 = MD5.Create())
            {
                while (true)
                {
                    try
                    {
                        using (var stream = File.OpenRead(file))
                        {
                            string hash = BitConverter.ToString(md5.ComputeHash(stream)).Replace("-", string.Empty);
                            md5Cache.Add(file, hash);
                            return hash;
                        }
                    }
                    catch
                    {
                        Thread.Sleep(100);
                    }
                }
            }
        }
    }
}