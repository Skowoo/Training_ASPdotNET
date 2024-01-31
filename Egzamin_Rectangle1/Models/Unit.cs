using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Egzamin_Rectangle1.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [Display(Name = "Jednostka")]
        public string Name { get; set; }

        public int Multiplier { get; set; }

        [InverseProperty("WidthUnit")]
        public ICollection<Rectangle>? WidthUnits { get; set; }

        [InverseProperty("HeightUnit")]
        public ICollection<Rectangle>? HeightUnits { get; set; }
    }
}
