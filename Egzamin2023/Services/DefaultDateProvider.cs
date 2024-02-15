namespace Egzamin2023.Services
{
    public class DefaultDateProvider : IDateProvider
    {
        public DateTime CurrentDate { get => DateTime.Now; }
    }
}
