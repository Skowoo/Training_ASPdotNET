using Egzamin_Rectangle2.Models;

namespace Egzamin_Rectangle2.Services
{
    public class RectangleService
    {
        List<Rectangle> _rects = new();

        public void Add(Rectangle input)
        {
            input.Id = _rects.Max(x => x.Id) + 1;
            input.Id ??= 1;
            input.CreatedAt = DateTime.Now;
            input.Area = (decimal)input.Height * input.Width * Convert.ToInt32(input.HeightUnit) * Convert.ToInt32(input.WidthUnit) / 1_000_000;
            _rects.Add(input);
        }

        public List<Rectangle> GetAll() => _rects;

        public Rectangle? GetById(int id)
        {
            if (_rects.Any(x => x.Id == id))
                return _rects.Single(x => x.Id == id);

            return null;
        }
    }
}
