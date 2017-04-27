using Discord.Commands;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using alpha.io.SQLite.Entities.Voice;

namespace alpha.io.SQLite
{
    public class VoiceDb : DbContext
    {
        public DbSet<LiteVoiceActivity> VoiceActivity { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "voice.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }

        public async Task AddVoiceActivityAsync(LiteVoiceActivity voiceActivity)
        {
            //var duplicate = await VoiceActivity.AnyAsync(x => x.UserId == voiceActivity.UserId && x.Timestamp == voiceActivity.Timestamp);
            //if (duplicate)
            //{
            //    Console.WriteLine($"Ignoring DB write for {voiceActivity.UserId} at {voiceActivity.Timestamp} due to duplicate");
            //    return;
            //}

            await VoiceActivity.AddAsync(voiceActivity);
            await SaveChangesAsync();
        }
    }
}
