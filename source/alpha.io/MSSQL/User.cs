using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace alpha.io.MSSQL
{
    [Table("io_users")]
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("discord_id")]
        public ulong DiscordId { get; set; }

        [Column("username")]
        public string Name { get; set; }

        [Column("poe_account")] public string PoEAccount { get; set; }
    }
}
