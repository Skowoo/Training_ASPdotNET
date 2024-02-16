using Egzamin_Rectangle2.Models;

namespace Egzamin_Rectangle2.Services
{
    public class MemoryRectangleService : IRectangleService
    {
        List<Rectangle> _rects = new();

        public int Add(Rectangle input)
        {
            input.Id = _rects.Max(x => x.Id) + 1;
            input.Id ??= 1;
            input.CreatedAt = DateTime.Now;
            input.Area = (decimal)input.Height * input.Width * (int)input.HeightUnit * (int)input.WidthUnit / 1_000_000;
            _rects.Add(input);
            return (int)input.Id;
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
