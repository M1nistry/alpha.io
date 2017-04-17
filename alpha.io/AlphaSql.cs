using System;
using System.Data.SQLite;

namespace alpha.io
{
    public static class AlphaSql
    {
        private static readonly string _dbPath = Environment.CurrentDirectory + @"\serverDB.s3db";
        private static SQLiteConnection Connection { get; set; }

        static AlphaSql()
        {
            //var constring = $@"Data Source={_dbPath};Version=3;";
            var constring = new SQLiteConnectionStringBuilder
            {
                DataSource = _dbPath,
                Version = 3,
            };
            Connection = new SQLiteConnection(constring.ToString());
            Connection.ParseViaFramework = true;
            if (InitDb(_dbPath))
            {

            }
        }

        private static bool InitDb(string dbPath)
        {
            if (!Environment.CurrentDirectory.Contains(@"serverDB.s3db"))
            {
                SQLiteConnection.CreateFile(Environment.CurrentDirectory + @"\serverDB.s3db");
            }
            try
            {
                using (var connection = new SQLiteConnection(Connection).OpenAndReturn())
                {
                    using (var cmd = new SQLiteCommand(connection))
                    {

                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS `servers` (`id` INTEGER PRIMARY KEY AUTOINCREMENT, `server_id` LONG, `admin_id` INTEGER, `join_date` DATETIME, `invited_by` INTEGER)";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"CREATE TABLE IF NOT EXISTS `voice_activity` (`guild_id` INTEGER, `user_id` INTEGER, `timestamp` TIMESTAMP, `action` TEXT, `channel` INTEGER)";
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return true;
        }
    }
}
