using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace Egzamin.Models;

public class AppDbContext: DbContext
{
    public string DbPath { get; }
    
    public AppDbContext()
    {
        var folder = Directory.GetParent(Environment.CurrentDirectory)?.FullName; 
        DbPath = System.IO.Path.Join(folder, "chinook.db");
        if (!File.Exists(DbPath))
        {
            DbPath = Path.Join(Directory.GetParent(DbPath)?.Parent?.Parent?.Parent?.FullName, "chinook.db");
        }
    }

    protected override void OnConfiguring(DbContextOptionsBuilder options) =>
        options.UseSqlite($"Data Source={DbPath}");

    public DbSet<Artist> Artists { get; set; }

    public DbSet<Album> Albums { get; set; }
}