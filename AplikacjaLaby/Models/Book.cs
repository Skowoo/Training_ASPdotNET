using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AplikacjaLaby.Models
{
    public class Book
    {
        [HiddenInput]
        public int Id { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "Zakres 3 - 20 znaków")]
        public string? Title { get; set; }

        [Required]
        [RegularExpression("[A-Z][a-z]{1,19} [A-Z][a-z]{1,19}", 
            ErrorMessage = "Imię i Nazwisko oddzielone spacją, każde może składać się wyłącznie z liter oddzielonych pojedynczą spacją i musi zaczynać się od wielkiej litery!")]
        public string? Author { get; set; }

        [Range(5, 2000, ErrorMessage = "zakres stron 5 - 2000")]
        public int Pages { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}-[0-9]{2}-[0-9]{2}-[0-9]{5}-[0-9]{1}", ErrorMessage = "Pożądany format: \"000-00-00-00000-0\"")]
        public string? ISBN { get; set; }

        [Range(800, int.MaxValue, ErrorMessage = "Minimalny rok to 800!")]
        public int PublishYear { get; set; }

        [StringLength(20, MinimumLength = 3, ErrorMessage = "Zakres 3 - 20 znaków")]
        public string? Publisher { get; set; }

        [Display(Name = "Dostępność")]
        public Availability Availability { get; set; }

        [HiddenInput]
        public DateTime Created { get; set; }


        [HiddenInput]
        public int OwnerId { get; set; }
        [ValidateNever]
        public List<SelectListItem> Owners { get; set; }
    }
}
