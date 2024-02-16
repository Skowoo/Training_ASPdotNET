using Egzamin_Rectangle2.Models;
using Microsoft.EntityFrameworkCore;

namespace Egzamin_Rectangle2.Data
{
    public class Context : DbContext
    {
        private string DbPath { get; set; } // Variable to store file path for SQLite Db

        public Context()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData; // Folder with local application data
            var path = Environment.GetFolderPath(folder); // Direct path to folder with local data
            DbPath = Path.Join(path, "exam.db"); // joining Db file name to path and assigning it to dedicated variable
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}"); // Defining use of SQLite with given file path

        public DbSet<Rectangle> Rectangles { get; set; }
    }
}

// Install Microsoft.EntityFrameworkCore.Sqlite
// Install Microsoft.EntityFrameworkCore.Design
// Create Context - look file
// Create new Service to use context
// NuGet console:
//  - Add-migration CreateDb
//  - update-database