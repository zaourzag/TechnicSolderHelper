using Mono.Data.Sqlite;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;

namespace TechnicSolderHelper.SQL.Forge
{
    public sealed class ForgeSqlHelper : SqlHelper
    {
        public ForgeSqlHelper() : base("forge")
        {
            //reset the table if it was generated before the Forge json URL was changed
            if (DoesFieldExist("type"))
            {
                ResetTable();
            }

            const string createTableString = "CREATE TABLE IF NOT EXISTS 'forge' ('totalversion' TEXT,'build' INTEGER UNIQUE, 'mcversion' TEXT, 'version' TEXT, 'downloadurl' TEXT, PRIMARY KEY(totalversion));";
            ExecuteDatabaseQuery(createTableString);
        }

        private void AddVersions(List<string> builds, List<string> mcversions, List<string> versions,
            List<string> downloadUrls)
        {
            string sql = string.Format("INSERT OR REPLACE INTO {0}('totalversion', 'build', 'mcversion', 'version', 'downloadurl') VALUES(@totalversion, @build, @mcversion, @version, @downloadurl);", TableName);

            if (IsUnix())
            {
                using (SqliteConnection db = new SqliteConnection(ConnectionString))
                {
                    db.Open();
                    using (SqliteCommand cmd = new SqliteCommand(sql, db))
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < builds.Count; i++)
                        {
                            sb.Clear();
                            sb.Append(mcversions[i])
                                .Append("-")
                                .Append(versions[i]);
                                //.Append("-")
                                //.Append(types[i]);
                            cmd.Parameters.AddWithValue("@totalversion", sb.ToString());
                            cmd.Parameters.AddWithValue("@build", builds[i]);
                            cmd.Parameters.AddWithValue("@mcversion", mcversions[i]);
                            cmd.Parameters.AddWithValue("@version", versions[i]);
                            cmd.Parameters.AddWithValue("@downloadurl", downloadUrls[i]);
                            //cmd.Parameters.AddWithValue("@type", types[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            else
            {
                using (SQLiteConnection db = new SQLiteConnection(ConnectionString))
                {
                    db.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                    {
                        StringBuilder sb = new StringBuilder();
                        for (int i = 0; i < builds.Count; i++)
                        {
                            sb.Clear();
                            sb.Append(mcversions[i])
                                .Append("-")
                                .Append(versions[i]);
                                //.Append("-")
                                //.Append(types[i]);
                            cmd.Parameters.AddWithValue("@totalversion", sb.ToString());
                            cmd.Parameters.AddWithValue("@build", builds[i]);
                            cmd.Parameters.AddWithValue("@mcversion", mcversions[i]);
                            cmd.Parameters.AddWithValue("@version", versions[i]);
                            cmd.Parameters.AddWithValue("@downloadurl", downloadUrls[i]);
                            //cmd.Parameters.AddWithValue("@type", types[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }

        public List<string> GetMcVersions()
        {
            string sql = "SELECT DISTINCT mcversion FROM " + TableName + " WHERE mcversion NOT LIKE '1.7.10_pre4' ORDER BY mcversion ASC;";
            List<string> mcVersion = new List<string>();
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
                                mcVersion.Add(reader["mcversion"].ToString());
                            }

                            return mcVersion;
                        }
                    }
                }
            }
            else
            {
                using (SQLiteConnection db = new SQLiteConnection(ConnectionString))
                {
                    db.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                mcVersion.Add(reader["mcversion"].ToString());
                            }

                            return mcVersion;
                        }
                    }
                }
            }
        }

        public List<string> GetForgeBuilds(string mcVersion)
        {
            string sql = string.Format("SELECT DISTINCT build FROM {0} WHERE mcversion LIKE '{1}' ORDER BY mcversion ASC;", TableName, mcVersion);
            List<string> forgeVersions = new List<string>();
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
                                forgeVersions.Add(reader["build"].ToString());
                            }

                            return forgeVersions;
                        }
                    }
                }
            }
            else
            {
                using (SQLiteConnection db = new SQLiteConnection(ConnectionString))
                {
                    db.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                forgeVersions.Add(reader["build"].ToString());
                            }

                            return forgeVersions;
                        }
                    }
                }
            }
        }

        public ForgeVersionInfo GetForgeInfo(string forgeVersion)
        {
            string sql = string.Format("SELECT * FROM {0} WHERE build LIKE '{1}';", TableName, forgeVersion);
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
                                ForgeVersionInfo build = new ForgeVersionInfo
                                {
                                    Build = int.Parse(reader["build"].ToString()),
                                    gameVersion = reader["mcversion"].ToString(),
                                    Version = reader["version"].ToString(),
                                    name = "forge-" + reader["version"].ToString(),
                                    DownloadUrl = reader["downloadurl"].ToString()
                                };
                                return build;
                            }
                        }
                    }
                }
                return new ForgeVersionInfo();
            }
            else
            {
                using (SQLiteConnection db = new SQLiteConnection(ConnectionString))
                {
                    db.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(sql, db))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ForgeVersionInfo build = new ForgeVersionInfo
                                {
                                    Build = int.Parse(reader["build"].ToString()),
                                    gameVersion = reader["mcversion"].ToString(),
                                    Version = reader["version"].ToString(),
                                    name = "forge-" + reader["version"].ToString(),
                                    DownloadUrl = reader["downloadurl"].ToString()
                                };
                                return build;
                            }
                        }
                    }
                }
            }

            return new ForgeVersionInfo();
        }

        public void FindAllForgeVersions()
        {
            List<string> builds = new List<string>();
            List<string> mcVersions = new List<string>();
            List<string> versions = new List<string>();
            //List<string> types = new List<string>();
            List<string> downloadUrls = new List<string>();
            ForgeJson forgeJson;
            HttpClient client = new HttpClient();
            Stream stream;
            try
            {
                //old forge maven URL: http://files.minecraftforge.net/maven/net/minecraftforge/forge/json
                //new forge maven URL: https://files.minecraftforge.net/maven/net/minecraftforge/forge/maven-metadata.json
                //curse URL: https://addons-ecs.forgesvc.net/api/v2/minecraft/modloader/
                stream = client.GetStreamAsync("https://addons-ecs.forgesvc.net/api/v2/minecraft/modloader/").Result;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error getting Forge versions.");
                Debug.WriteLine(e);

                MessageBox.Show("Error getting Forge versions.");
                return;
            }

            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
            {
                JsonSerializer serializer = new JsonSerializer();

                // read the json from a stream
                // json size doesn't matter because only a small piece is read at a time from the HTTP request
                forgeJson = serializer.Deserialize<ForgeJson>(jsonReader);
                jsonReader.Close();
            }

            forgeJson.WebPath = "http://files.minecraftforge.net/maven/net/minecraftforge/forge";

            foreach (ForgeVersionInfo forgeVersionInfo in forgeJson)
            {
                string mcVersion = forgeVersionInfo.gameVersion;
                string version = forgeVersionInfo.name.TrimStart("forge-".ToCharArray());
                string build = version.Substring(version.LastIndexOf('.') + 1);
                int buildInt;
                if (!int.TryParse(build, out buildInt))
                {
                    MessageBox.Show("Error parsing Forge versions");
                    return;
                }
                //string branch = forgeJson.ForgeVersionInfo[i].Branch;
                //string downloadUrl = string.Format("{0}/{1}-{2}{3}/forge-{1}-{2}{3}-",
                //    forgeJson.WebPath, mcVersion, version, string.IsNullOrWhiteSpace(branch) ? "" : "-" + branch);
                string downloadUrl = string.Format("{0}/{1}-{2}/forge-{1}-{2}-", forgeJson.WebPath, mcVersion, version);

                if (buildInt < 183)
                {
                    downloadUrl += "client";
                }
                else
                {
                    downloadUrl += "universal";
                }

                if (buildInt < 752)
                {
                    downloadUrl += ".zip";
                }
                else
                {
                    downloadUrl += ".jar";
                }
                mcVersions.Add(mcVersion);
                builds.Add(build);
                versions.Add(version);
                downloadUrls.Add(downloadUrl);
                //types.Add("universal");
            }

            AddVersions(builds, mcVersions, versions, downloadUrls);
        }

    }
}
