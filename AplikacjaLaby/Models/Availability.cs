using System.ComponentModel.DataAnnotations;

namespace AplikacjaLaby.Models
{
    public enum Availability
    {
        [Display(Name = "Brak")]
        None = 0,
        [Display(Name = "Niska")]
        Low = 1,
        [Display(Name = "Średnia")]
        Normal = 2,
        [Display(Name = "Wysoka")]
        High = 3
    }
}
