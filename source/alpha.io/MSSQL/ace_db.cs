using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace alpha.io.MSSQL
{
    [DbConfigurationType(typeof(DbConfiguration))]
    
    public class ace_db : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Guild> Guilds { get; set; }
        public DbSet<Channel> Channels { get; set; }

        public ace_db() : base("AceDb")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Channel>().HasKey(t => t.Id);
            modelBuilder.Entity<Guild>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<User>().HasKey(t => t.Id).Property(t => t.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            
        }

    }
}
