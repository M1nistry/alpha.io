using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alpha.io.MSSQL
{
    [Table("io_guilds")]
    public class Guild
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("guild_id")]
        public long GuildId { get; set; }

        [Column("owner_id")]
        public long OwnerId { get; set; }

        [Column("name")]
        public string Name { get; set; }
    }
}
