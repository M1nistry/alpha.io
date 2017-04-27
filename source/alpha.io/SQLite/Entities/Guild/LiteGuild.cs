using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alpha.io.SQLite.Entities.Guild
{
    public class LiteGuild : LiteEntity<ulong>
    {
        [Required]
        [Index("IX_GuildID_UNQ", 1, IsUnique= true)]
        public ulong GuildId { get; set; }
        [Required]
        public ulong OwnerId { get; set; }
        public string GuildName { get; set; }

        public LiteGuild(ulong guildId, ulong ownerId, string guildName)
        {
            GuildId = guildId;
            OwnerId = ownerId;
            GuildName = guildName;
        }

        public LiteGuild()
        {
            
        }
    }
}
