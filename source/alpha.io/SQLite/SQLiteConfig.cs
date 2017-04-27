using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha.io.SQLite
{
    public class SQLiteConfig : ConfigurationBase
    {
        public int RelatedTagsLimit { get; set; } = 3;

        public SQLiteConfig() : base("config/sqlite_config.json") { }

        public static SQLiteConfig Load()
        {
            EnsureExists();
            return Load<SQLiteConfig>();
        }

        public static void EnsureExists()
        {
            string file = Path.Combine(AppContext.BaseDirectory, FileName);
            if (!File.Exists(file))
            {
                string path = Path.GetDirectoryName(file);
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);

                var config = new SQLiteConfig();
                config.SaveJson();
            }
        }
    }
}
