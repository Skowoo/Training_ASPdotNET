using System.Net;

namespace AplikacjaLabyData.Entities
{
    public class OwnerEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public Address? Address { get; set; }
        public ISet<BookEntity>? Books { get; set; }
    }
}
