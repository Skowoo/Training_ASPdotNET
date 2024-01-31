using System.ComponentModel.DataAnnotations;

namespace Egzamin_Rectangle1.Models
{
    public class Rectangle
    {
        public int Id { get; set; }

        [Required]
        [Range(1, 1000000)]
        [Display(Name = "Szerokość")]
        public int Width { get; set; }

        public int? WidthUnitId { get; set; }
        public Unit? WidthUnit { get; set; }

        [Required]
        [Range(1, 1000000)]
        [Display(Name = "Wysokość")]
        public int Height { get; set; }

        public int? HeightUnitId { get; set; }
        public Unit? HeightUnit { get; set; }
    }
}
