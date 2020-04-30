using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TechnicSolderHelper.SQL.WorkTogether
{
    class DataSuggest
    {
        private readonly string _connectionStringSuggest;
        private readonly string _connectionStringGet;
        private const string SuggestDatabase = "solderhelper";
        private const string GetDatabase = "helpersolder";

        public DataSuggest()
        {
            _connectionStringSuggest = string.Format("address=zlepper.dk;username=dataSuggester;password=suggest;database={0}", SuggestDatabase);
            _connectionStringGet = string.Format("address=zlepper.dk;username=dataSuggester;password=suggest;database={0}", GetDatabase);
        }

        public void Suggest(string filename, string mcversion, string modversion, string md5, string modid, string modname, string author = "")
        {
            try
            {
                if (!IsModSuggested(md5))
                {
                    const string sql =
                        "INSERT INTO solderhelper.new(filename, mcversion, modversion, md5, modid, modname, author) VALUES(@filename, @mcversion, @modversion, @md5, @modid, @modname, @author);";
                    using (MySqlConnection connection = new MySqlConnection(_connectionStringSuggest))
                    {
                        connection.OpenAsync();
                        using (MySqlCommand command = new MySqlCommand(sql, connection))
                        {
                            command.Parameters.AddWithValue("@filename", filename);
                            command.Parameters.AddWithValue("@mcversion", mcversion);
                            command.Parameters.AddWithValue("@modversion", modversion);
                            command.Parameters.AddWithValue("@md5", md5);
                            command.Parameters.AddWithValue("@modid", modid);
                            command.Parameters.AddWithValue("@modname", modname);
                            command.Parameters.AddWithValue("@author", author);
                            command.ExecuteNonQueryAsync();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                if (e.InnerException != null)
                {
                    Debug.WriteLine(e.InnerException.Message);
                    Debug.WriteLine(e.InnerException.StackTrace);

                }
            }
        }

        public bool IsModSuggested(string md5)
        {
            try
            {
                string sql = "SELECT md5 FROM solderhelper.new WHERE md5 LIKE @md5;";
                using (MySqlConnection connection = new MySqlConnection(_connectionStringSuggest))
                {
                    connection.OpenAsync();
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@md5", md5);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["md5"].Equals(md5))
                                    return true;
                                return false;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                if (e.InnerException != null)
                {
                    Debug.WriteLine(e.InnerException.Message);
                    Debug.WriteLine(e.InnerException.StackTrace);

                }
            }
            return false;
        }

        public McMod GetMcmod(string md5)
        {
            try
            {
                string sql =
                    "SELECT modname, modid, mcversion, modversion, md5, author FROM helpersolder.mods WHERE md5 LIKE @md5;";
                using (MySqlConnection connection = new MySqlConnection(_connectionStringGet))
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@md5", md5);
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (reader["md5"].Equals(md5))
                                {
                                    List<string> a = reader["author"].ToString().Split(',').ToList();

                                    McMod mod = new McMod
                                    {
                                        Version = reader["modversion"].ToString(),
                                        Name = reader["modname"].ToString(),
                                        modId = reader["modid"].ToString(),
                                        McVersion = reader["mcversion"].ToString(),
                                        Authors = a,
                                        AuthorList = a

                                    };
                                    return mod;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
                if (e.InnerException != null)
                {
                    Debug.WriteLine(e.InnerException.Message);
                    Debug.WriteLine(e.InnerException.StackTrace);

                }
            }
            return null;
        }
    }
}
