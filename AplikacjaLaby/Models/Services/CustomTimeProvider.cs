
namespace AplikacjaLaby.Models.Services
{
    public class CustomTimeProvider : ITimeProvider
    {
        public DateTime GetCurrentTime() => DateTime.Now;
    }
}
