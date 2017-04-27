using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using alpha.io.SQLite.Entities.User;
using Discord.WebSocket;
using Microsoft.EntityFrameworkCore;

namespace alpha.io.SQLite
{
    public class UserDb : DbContext
    {
        public DbSet<LiteUser> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string baseDir = Path.Combine(AppContext.BaseDirectory, "data");
            if (!Directory.Exists(baseDir))
                Directory.CreateDirectory(baseDir);

            string datadir = Path.Combine(baseDir, "users.sqlite.db");
            optionsBuilder.UseSqlite($"Filename={datadir}");
        }

        public async Task AddOrUpdateUsersAsync(IEnumerable<SocketGuild> guilds)
        {
            var liteUsers = new List<LiteUser>();
            foreach (var guild in guilds)
            {
                foreach (var user in guild.Users)
                {
                    var exist = await Users.AnyAsync(x => x.UserId == user.Id);
                    if (exist)
                    {
                        Users.Update(new LiteUser
                        {
                          UserId  = user.Id,
                          UserName = user.Username
                        });
                        await SaveChangesAsync();
                        continue;
                    }
                    liteUsers.Add(new LiteUser
                    {
                        UserId = user.Id,
                        UserName = user.Username
                    });
                }
            }
            await Users.AddRangeAsync(liteUsers);
            await SaveChangesAsync();
        }

        public async Task<string> GetUsernameAsync(ulong userId)
        {
            var user = await Users.FirstAsync(x => x.UserId == userId);
            return user.UserName;
        }
    }
}
