using Mono.Data.Sqlite;
using System.Data.SQLite;
using TechnicSolderHelper.SQL;

namespace TechnicSolderHelper.Confighandler
{
    public class ConfigHandler : SqlHelper
    {
        private readonly string _createTableString;

        public ConfigHandler()
            : base("configs")
        {
            _createTableString =
                string.Format("CREATE TABLE IF NOT EXISTS `{0}` (`key` TEXT NOT NULL UNIQUE, `value` TEXT);", TableName);
            ExecuteDatabaseQuery(_createTableString);
        }

        public string GetConfig(string configName)
        {
            string sql = string.Format("SELECT value FROM {0} WHERE key LIKE @key;", TableName);
            if (Globalfunctions.IsUnix())
            {
                using (SqliteConnection db = new SqliteConnection(ConnectionString))
                {
                    db.Open();
                    using (SqliteCommand cmd = new SqliteCommand(sql, db))
                    {
                        cmd.Parameters.AddWithValue("@key", configName);
                        using (SqliteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return reader["value"].ToString();
                            }
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
                        cmd.Parameters.AddWithValue("@key", configName);
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return reader["value"].ToString();
                            }
                        }
                    }
                }
            }
            return string.Empty;
        }

        public void SetConfig(string configName, bool configValue)
        {
            SetConfig(configName, configValue.ToString());
        }

        public void SetConfig(string configName, string configValue)
        {
            string sql = string.Format("INSERT OR REPLACE INTO {0}(key, value) VALUES(@key, @value);", TableName);
            Debug.WriteLine(sql);
            if (Globalfunctions.IsUnix())
            {
                using (SqliteConnection db = new SqliteConnection(ConnectionString))
                {
                    db.Open();
                    using (SqliteCommand cmd = new SqliteCommand(sql, db))
                    {
                        cmd.Parameters.AddWithValue("@key", configName);
                        cmd.Parameters.AddWithValue("@value", configValue);
                        cmd.ExecuteNonQuery();
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
                        cmd.Parameters.AddWithValue("@key", configName);
                        cmd.Parameters.AddWithValue("@value", configValue);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public override void ResetTable()
        {
            base.ResetTable();
            ExecuteDatabaseQuery(_createTableString);
        }
    }
}
