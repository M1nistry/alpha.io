using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alpha.io.SQLite.Entities.Guild
{
    public class LiteGuildChannel : LiteEntity<ulong>
    {
        [Required]
        [Index("IX_ChannelID_UNQ", 1, IsUnique = true)]
        public ulong ChannelId { get; set; }
        [Required]
        public ulong GuildId { get; set; }
        public string ChannelName { get; set; }

        public LiteGuildChannel(ulong channelId, string channelName, ulong guildId)
        {
            ChannelId = channelId;
            ChannelName = channelName;
            GuildId = guildId;
        }
    }
}
