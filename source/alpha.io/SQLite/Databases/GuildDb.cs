using Discord.Commands;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using alpha.io.SQLite.Entities.Guild;
using alpha.io.SQLite.Entities.User;

namespace alpha.io.SQLite
{
    public class GuildDb : DbContext
    {
        public DbSet<LiteGuild> Guilds { get; set; }
        public DbSet<LiteGuildChannel> Channels { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "guilds.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }

        public Task<LiteGuild> GetGuildAsync(ulong guildId) => Guilds.FirstOrDefaultAsync(x => x.GuildId == guildId);

        public async Task AddGuildsAsync(IEnumerable<SocketGuild> guilds)
        {
            var liteGuilds = new List<LiteGuild>();
            var liteChannels = new List<LiteGuildChannel>();
            foreach (var guild in guilds)
            {
                var exists = await Guilds.AnyAsync(x => x.GuildId == guild.Id);
                if (exists)
                {
                    //await UpdateGuildAsync(guild);
                    continue;
                }
                liteGuilds.Add(new LiteGuild(guild.Id, guild.OwnerId, guild.Name));
                liteChannels.AddRange(guild.Channels.Select(channel => new LiteGuildChannel(channel.Id, channel.Name, guild.Id)));
            }
            
            await Guilds.AddRangeAsync(liteGuilds);
            await Channels.AddRangeAsync(liteChannels);
            await SaveChangesAsync();
        }

        public async Task UpdateGuildAsync(SocketGuild guild)
        {
            var liteChannels = new List<LiteGuildChannel>();
            liteChannels.AddRange(guild.Channels.Select(channel => new LiteGuildChannel(channel.Id, channel.Name, guild.Id)));

            Channels.AttachRange(liteChannels);
            Channels.UpdateRange(liteChannels);
            await SaveChangesAsync();
        }

        public async Task GetGuildId(ulong channelId)
        {
            
        }
    }

   
}
