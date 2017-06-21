using System;
using System.IO;
using System.Threading.Tasks;
using alpha.io.SQLite.Entities.Message;
using Microsoft.EntityFrameworkCore;

namespace alpha.io.SQLite
{
    public class MessageDb : DbContext
    {
        public DbSet<LiteMessage> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "messages.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }
        public async Task AddMessageAsync(LiteMessage message)
        {
            //var duplicate = await VoiceActivity.AnyAsync(x => x.UserId == voiceActivity.UserId && x.Timestamp == voiceActivity.Timestamp);
            //if (duplicate)
            //{
            //    Console.WriteLine($"Ignoring DB write for {voiceActivity.UserId} at {voiceActivity.Timestamp} due to duplicate");
            //    return;
            //}

            await Messages.AddAsync(message);
            await SaveChangesAsync();
        }
    }
}
