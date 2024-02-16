using Egzamin_Rectangle2.Models;

namespace Egzamin_Rectangle2.Services
{
    public interface IRectangleService
    {
        public int Add(Rectangle input);

        public List<Rectangle> GetAll();

        public Rectangle? GetById(int id);

    }
}