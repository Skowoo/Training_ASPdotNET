using DbFirst1.Data;
using DbFirst1.Models;

namespace DbFirst1.Services
{
    public class SQLiteService : IProductService
    {
        private ApplicationDbContext _context;

        public SQLiteService(ApplicationDbContext adbc)
        {
            _context = adbc;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
