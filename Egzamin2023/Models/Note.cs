using System.ComponentModel.DataAnnotations;

namespace Egzamin2023.Models
{
    // Imie Nazwisko Indeks

    public class Note
    {
        [Display(Name = "Tytuł")]
        [StringLength(20, MinimumLength = 3)]
        public string Title { get; set; }

        [Display(Name = "Treść")]
        [StringLength(2000, MinimumLength = 10)]
        public string Content { get; set; }

        [Display(Name = "Data ważności")]
        public DateTime Deadline { get; set; }
    }
}
