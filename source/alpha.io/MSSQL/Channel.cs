using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace alpha.io.MSSQL
{
    [Table("io_channels")]
    public class Channel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key, Column("id", Order = 0)]
        public int Id { get; set; }

        [Column("channel_id")]
        public ulong ChannelId { get; set; }

        [Column("guild_id")]
        public ulong GuildId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
