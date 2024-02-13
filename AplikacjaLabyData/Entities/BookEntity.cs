using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AplikacjaLabyData.Entities
{
    [Table("books")]
    public class BookEntity
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string? Title { get; set; }

        [MaxLength(41)]
        public string? Author { get; set; }

        public int Pages { get; set; }

        [MaxLength(17)]
        public string? ISBN { get; set; }

        [Column("publish_year")]
        public int PublishYear { get; set; }

        public string? Publisher { get; set; }

        public int Availability { get; set; }

        public DateTime Created { get; set; }

        public int OwnerId { get; set; }
        public OwnerEntity? Owner { get; set; }
    }
}
