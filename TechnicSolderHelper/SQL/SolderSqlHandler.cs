using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TechnicSolderHelper.Confighandler;
using TechnicSolderHelper.Cryptography;

namespace TechnicSolderHelper.SQL
{

    public class SolderSqlHandler
    {
        private readonly string _connectionString;
        private readonly string _database;
        private readonly string _prefix;

        public SolderSqlHandler(string address, string username, string password, string database, string prefix = "")
        {
            _connectionString = string.Format("address={0};username={1};password={2};database={3}", address, username, password, database);
            _database = database;
            _prefix = prefix;
        }

        public SolderSqlHandler()
        {
            Crypto crypto = new Crypto();
            ConfigHandler ch = new ConfigHandler();
            try
            {
                string s = ch.GetConfig("mysqlPassword");
                if (string.IsNullOrWhiteSpace(s))
                {
                    ch.SetConfig("mysqlPassword", crypto.EncryptToString("password"));
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.InnerException.ToString());
                ch.SetConfig("mysqlPassword", crypto.EncryptToString("password"));
            }
            var password = crypto.DecryptString(ch.GetConfig("mysqlPassword"));
            var username = ch.GetConfig("mysqlUsername");
            var address = ch.GetConfig("mysqlAddress");
            _database = ch.GetConfig("mysqlDatabase");
            _prefix = ch.GetConfig("mysqlPrefix");
            _connectionString = string.Format("address={0};username={1};password={2};database={3}", address, username, password, _database);
        }

        /// <summary>
        /// Checks if the current connection works and can find a solder database
        /// </summary>
        /// <returns>True if the connections worked, otherwise false</returns>
        public void TestConnection()
        {
            List<string> tables = new List<string>
            {
                _prefix + "modversions",
                _prefix + "mods",
                _prefix + "modpacks",
                _prefix + "clients",
                _prefix + "client_modpack",
                _prefix + "builds",
                _prefix + "build_modversion"
            };
            try
            {
                string sql = string.Format("SHOW TABLES IN {0};", _database);
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            string s = "Tables_in_" + _database;
                            while (reader.Read())
                            {
                                tables.Remove(reader[s].ToString());
                            }
                            if (tables.Count == 0)
                            {
                                MessageBox.Show("The database is alright");
                                return;
                            }
                            MessageBox.Show("Some tables appears to be missing in the database. Please reconstruct it and try again.");
                        }
                    }
                }
            }
            catch (MySqlException e)
            {
                MessageBox.Show(e.Message);
                Debug.WriteLine(e.Message);
                //Debug.WriteLine(e.InnerException.ToString());
            }
        }

        public void UpdateModVersionMd5(string modslug, string modversion, string md5)
        {
            int id = GetModId(modslug);
            string sql = string.Format("UPDATE {0}.{1} SET md5=@md5 , updated_at=@update WHERE version LIKE @modversion AND mod_id LIKE @modid;", _database, _prefix + "modversions");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@md5", md5);
                    cmd.Parameters.AddWithValue("@modversion", modversion);
                    cmd.Parameters.AddWithValue("@modid", id);
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Checks if a certain modpack exists.
        /// </summary>
        /// <param name="modpackName">The name of the modpack</param>
        /// <returns>True if the modpack exists, otherwise false.</returns>
        public int GetModpackId(string modpackName)
        {
            string sql = string.Format("SELECT id FROM {0}.{1} WHERE slug LIKE @modpack OR name LIKE @modpack", _database, _prefix + "modpacks");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@modpack", modpackName);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["id"]);
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Adds a mod to solder.
        /// </summary>
        /// <param name="modslug">The slug. Cannot be null</param>
        /// <param name="description">The description of the mod. Can be null</param>
        /// <param name="author">The name of the mod author. Can be null</param>
        /// <param name="link">The link to the mod. Can be null</param>
        /// <param name="name">The pretty name of the mod. Cannot be null</param>
        public void AddModToSolder(string modslug, string description, string author, string link, string name)
        {
            string sql = string.Format("INSERT INTO {0}.{1}(name, description, author, link, pretty_name, created_at, updated_at) VALUES(@modslug, @descriptionValue, @authorValue, @linkValue, @name, @create, @update);", _database, _prefix + "mods");

            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@modslug", modslug);
                    cmd.Parameters.AddWithValue("@name", name);
                    cmd.Parameters.AddWithValue("@create", DateTime.Now);
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);
                    cmd.Parameters.AddWithValue("@descriptionValue",
                        string.IsNullOrWhiteSpace(description) ? "" : description);
                    cmd.Parameters.AddWithValue("@authorValue", string.IsNullOrWhiteSpace(author) ? "" : author);
                    cmd.Parameters.AddWithValue("@linkValue", string.IsNullOrWhiteSpace(link) ? "" : link);
                    if (GetModId(modslug) == -1)
                        cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Get the id of a mod on solder.
        /// </summary>
        /// <param name="slug">The modslug of the mod. Also known as the slug</param>
        /// <returns>Returns the modslug of the mod if found, otherwise returns -1.</returns>
        public int GetModId(string slug)
        {
            string sql = string.Format("SELECT id FROM {0}.{1} WHERE name LIKE @modname", _database, _prefix + "mods");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@modname", slug);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["id"]);
                        }
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Checks if a certain mod version is already on Solder.
        /// </summary>
        /// <param name="modid">The modslug</param>
        /// <param name="version">The version</param>
        /// <returns>Returns true if the mod version is on solder, false if not. </returns>
        private bool IsModVersionOnline(int modid, string version)
        {
            string sql = string.Format("SELECT id FROM {0}.{1} WHERE version LIKE @version AND mod_id LIKE @modslug;", _database, _prefix + "modversions");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@version", version);
                    cmd.Parameters.AddWithValue("@modslug", modid);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if a certain mod version is already on Solder.
        /// </summary>
        /// <param name="modid">The modslug</param>
        /// <param name="version">The version</param>
        /// <returns>Returns true if the mod version is on solder, false if not. </returns>
        public bool IsModVersionOnline(string modid, string version)
        {
            int id = GetModId(modid);
            if (id == -1) return false;
            return IsModVersionOnline(id, version);
        }

        /// <summary>
        /// Adds a new mod version to Solder.
        /// </summary>
        /// <param name="modid">The modslug</param>
        /// <param name="version">The mod version</param>
        /// <param name="md5">The MD5 value of the zip</param>
        public void AddNewModVersionToSolder(int modid, string version, string md5)
        {
            if (IsModVersionOnline(modid, version))
                return;
            string sql = string.Format("INSERT INTO {0}.{1}(mod_id, version, md5, created_at, updated_at) VALUES(@modslug, @version, @md5, @create, @update);", _database, _prefix + "modversions");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@modslug", modid);
                    cmd.Parameters.AddWithValue("@version", version);
                    cmd.Parameters.AddWithValue("@md5", md5);
                    cmd.Parameters.AddWithValue("@create", DateTime.Now);
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// Adds a new mod version to Solder.
        /// </summary>
        /// <param name="modid">The modslug</param>
        /// <param name="version">The mod version</param>
        /// <param name="md5">The MD5 value of the zip</param>
        public void AddNewModVersionToSolder(string modid, string version, string md5)
        {
            int id = GetModId(modid);
            AddNewModVersionToSolder(id, version, md5);
        }

        public void CreateNewModpack(string modpackName, string modpackSlug)
        {
            string sql = string.Format("INSERT INTO {0}.{1}(name, slug, created_at, updated_at, icon_md5, logo_md5, background_md5, recommended, latest, `order`, hidden, private, icon, logo, background) VALUES(@name, @slug, @create, @update, \"\",\"\",\"\",\"\",\"\", 0,1,0,0,0,0);", _database, _prefix + "modpacks");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@name", modpackName);
                    cmd.Parameters.AddWithValue("@slug", modpackSlug);
                    cmd.Parameters.AddWithValue("@create", DateTime.Now);
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CreateModpackBuild(int modpackId, string version, string mcVersion, string javaVersion, int memory)
        {
            string sql = string.Format("INSERT INTO {0}.{1}(modpack_id, version, minecraft, is_published, private, created_at, updated_at, min_java, min_memory) VALUES(@modpack, @version, @mcVersion, 0, 0, @create, @update, @minJava, @minMemory);", _database, _prefix + "builds");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@modpack", modpackId);
                    cmd.Parameters.AddWithValue("@version", version);
                    cmd.Parameters.AddWithValue("@mcVersion", mcVersion);
                    cmd.Parameters.AddWithValue("@create", DateTime.Now);
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);
                    cmd.Parameters.AddWithValue("@minJava", string.IsNullOrWhiteSpace(javaVersion) ? "" : javaVersion);
                    cmd.Parameters.AddWithValue("@minMemory", memory);
                    cmd.ExecuteNonQuery();
                }
                sql = string.Format("UPDATE {0}.{1} SET updated_at=@update WHERE id LIKE @id;", _database, _prefix + "modpacks");
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@update", DateTime.Now);
                    cmd.Parameters.AddWithValue("@id", modpackId);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int GetBuildId(int modpackId, string version)
        {
            string sql = string.Format("SELECT id FROM {0}.{1} WHERE modpack_id LIKE @modpack AND version LIKE @version;", _database, _prefix + "builds");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@modpack", modpackId);
                    cmd.Parameters.AddWithValue("@version", version);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["id"].ToString());
                        }
                    }
                }
            }
            return -1;
        }

        public int GetModVersionId(int modId, string version)
        {
            string sql = string.Format("SELECT id FROM {0}.{1} WHERE mod_id LIKE @mod AND version LIKE @version", _database, _prefix + "modversions");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@mod", modId);
                    cmd.Parameters.AddWithValue("@version", version);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["id"].ToString());
                        }
                    }
                }
            }

            return -1;
        }

        private bool IsModVersionInBuild(int build, int modversionId)
        {
            string sql = string.Format("SELECT id FROM {0}.{1} WHERE modversion_id LIKE @version AND build_id LIKE @build;", _database, _prefix + "build_modversion");
            using (MySqlConnection conn = new MySqlConnection(_connectionString))
            {
                conn.Open();
                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@version", modversionId);
                    cmd.Parameters.AddWithValue("@build", build);
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void AddModVersionToBuild(int build, int modversionId)
        {
            if (!IsModVersionInBuild(build, modversionId))
            {
                string sql = string.Format("INSERT INTO {0}.{1}(modversion_id, build_id, created_at, updated_at) VALUES(@modslug, @buildid, @created, @updated);", _database, _prefix + "build_modversion");
                using (MySqlConnection conn = new MySqlConnection(_connectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@modslug", modversionId);
                        cmd.Parameters.AddWithValue("@buildid", build);
                        cmd.Parameters.AddWithValue("@created", DateTime.Now);
                        cmd.Parameters.AddWithValue("@updated", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                    sql = string.Format("UPDATE {0}.{1} SET updated_at=@update WHERE id LIKE @id;", _database, _prefix + "builds");
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", build);
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);
                        cmd.ExecuteNonQuery();
                    }
                    int modpackid;
                    sql = string.Format("SELECT modpack_id FROM {0}.{1} WHERE id LIKE @buildid;", _database, _prefix + "builds");
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@buildid", build);
                        using (MySqlDataReader reader = cmd.ExecuteReader())
                        {
                            reader.Read();

                            modpackid = Convert.ToInt32(reader["modpack_id"].ToString());

                        }
                    }
                    sql = string.Format("UPDATE {0}.{1} SET updated_at=@update WHERE id LIKE @modpackid;", _database, _prefix + "modpacks");
                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@update", DateTime.Now);
                        cmd.Parameters.AddWithValue("@modpackid", modpackid);
                        cmd.ExecuteNonQuery();
                    }
                }
            }

        }
    }
}
