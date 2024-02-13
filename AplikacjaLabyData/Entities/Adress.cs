#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace AplikacjaLabyData.Entities
{
    public class Address
    {
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public string Region { get; set; }
    }
}
