using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace alpha.io.SQLite.Entities.User
{
    public class LiteUser : LiteEntity<ulong>
    {
        [Required]
        [Index("IX_UserID_UNQ", 1, IsUnique = true)]
        public ulong UserId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
