using Egzamin_Rectangle2.Data;
using Egzamin_Rectangle2.Models;

namespace Egzamin_Rectangle2.Services
{
    public class SQLiteService : IRectangleService
    {
        private Context _context;

        public SQLiteService(Context ct)
        {
            _context = ct;
        }

        public int Add(Rectangle input)
        {
            input.Id = _context.Rectangles.Max(x => x.Id) + 1;
            input.Id ??= 1;
            input.CreatedAt = DateTime.Now;
            input.Area = (decimal)input.Height * input.Width * (int)input.HeightUnit * (int)input.WidthUnit / 1_000_000;
            _context.Rectangles.Add(input);
            _context.SaveChanges();
            return (int)input.Id;
        }

        public List<Rectangle> GetAll() => _context.Rectangles.ToList();

        public Rectangle? GetById(int id)
        {
            if (_context.Rectangles.Any(x => x.Id == id))
                return _context.Rectangles.Single(x => x.Id == id);

            return null;
        }
    }
}
