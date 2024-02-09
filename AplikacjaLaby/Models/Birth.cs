namespace AplikacjaLaby.Models
{
    public class Birth
    {
        public string? Name { get; set; }

        public string? BirthDate { get; set; }

        public bool IsValid()
        {
            if (BirthDate is null || Name is null)
                return false;

            var parsing = DateTime.TryParse(BirthDate, out DateTime result);

            if (!parsing) 
                return false;

            return result < DateTime.Now;
        }

        public int GetAge() => DateTime.Now.Year - DateTime.Parse(BirthDate!).Year;
    }
}
