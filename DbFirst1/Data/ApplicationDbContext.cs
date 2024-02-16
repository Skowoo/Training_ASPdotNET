using DbFirst1.Models;
using Microsoft.EntityFrameworkCore;

namespace DbFirst1.Data
{
    public class ApplicationDbContext : DbContext
    {
        // DbSet for table
        public DbSet<Product> Products { get; set; }

        // Ctor for SQLite - with DBPath
        private string DbPath { get; set; } // Variable to store file path for SQLite Db
        public ApplicationDbContext()
        {
            var path = "C:\\Users\\universal\\Desktop"; // Path to file on desktop
            DbPath = Path.Join(path, "TestDB.db"); // joining Db file name to path and assigning it to dedicated variable
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}"); // Defining use of SQLite with given file path
    }
}
