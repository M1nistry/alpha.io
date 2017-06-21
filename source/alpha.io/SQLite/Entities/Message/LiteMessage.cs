using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha.io.SQLite.Entities.Message
{
    public class LiteMessage : LiteEntity<ulong>
    {
        [Required]
        public DateTime TimeStamp { get; set; } = DateTime.Now;
        [Required]
        public ulong GuildId { get; set; }
        [Required]
        public ulong UserId { get; set; }
        [Required]
        public ulong ChannelId { get; set; }

        public LiteMessage(ulong guildId, ulong userId, ulong channelId)
        {
            GuildId = guildId;
            UserId = userId;
            ChannelId = channelId;
        }
    }
}
