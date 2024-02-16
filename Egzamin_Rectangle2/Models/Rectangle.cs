using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Egzamin_Rectangle2.Models
{
    public class Rectangle
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Range(0, 1000000)]
        public int Width { get; set; }

        [Range(0, 1000000)]
        public int Height { get; set; }

        [HiddenInput]
        public Unit WidthUnit { get; set; }

        [HiddenInput]
        public Unit HeightUnit { get; set; }

        [HiddenInput]
        public decimal? Area { get; set; }

        [HiddenInput]
        public DateTime? CreatedAt { get; set; }
    }

    public enum Unit
    {
        m = 1000,
        cm = 100,
        mm = 1,
    }
}
