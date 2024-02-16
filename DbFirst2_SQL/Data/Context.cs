using DbFirst2_SQL.Models;
using Microsoft.EntityFrameworkCore;

namespace DbFirst2_SQL.Data
{
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }

        // ctor passing options to base class
        public Context(DbContextOptions<Context> options) : base(options) { }
    }
}
