using System.ComponentModel.DataAnnotations;

namespace alpha.io.SQLite.Entities
{
    public abstract class LiteEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}
