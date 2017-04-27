using System.ComponentModel.DataAnnotations;

namespace alpha.io.SQLite.Entities.User
{
    public class LiteUser : LiteEntity<ulong>
    {
        [Required]
        public ulong UserId { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}
