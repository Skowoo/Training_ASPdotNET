using DbFirst2_SQL.Data;
using DbFirst2_SQL.Models;

namespace DbFirst2_SQL.Services
{
    public class SqlService : IService
    {
        private static Context _context;
        public SqlService(Context ct)
        {
            _context = ct;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}
