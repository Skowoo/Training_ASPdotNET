using AplikacjaLabyData.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AplikacjaLabyData
{
    public class AppDbContext : DbContext
    {
        public DbSet<BookEntity> Books { get; set; }

        public DbSet<OwnerEntity> Owners { get; set; }

        private string DbPath { get; set; } // Variable to store file path for SQLite Db

        public AppDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData; // Folder with local application data
            var path = Environment.GetFolderPath(folder); // Direct path to folder with local data
            DbPath = System.IO.Path.Join(path, "books.db"); // joining Db file name to path and assigning it to dedicated variable
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options) =>
            options.UseSqlite($"Data Source={DbPath}"); // Defining use of SQLite with given file path

        protected override void OnModelCreating(ModelBuilder modelBuilder) // Function which helps to create DB
        {
            modelBuilder.Entity<BookEntity>() // Define relations between entitities
                .HasOne(e => e.Owner)
                .WithMany(e => e.Books)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<OwnerEntity>() //Define that OwnerEntity have one Adress.                
                .HasData(
                new OwnerEntity // Initial date for Owners
                {
                    Id = 1,
                    Name = "Pawel",
                    Surname = "Bielecki"
                },
                new OwnerEntity
                {
                    Id = 2,
                    Name = "Jacek",
                    Surname = "Obrzut"
                }
                );

            modelBuilder.Entity<BookEntity>()
                .HasData( // Initial data for Books
                new BookEntity
                {
                    Id = 1,
                    Author = "Janusz Tracz",
                    ISBN = "321-21-21-54321-2",
                    Pages = 200,
                    Publisher = "Rebis",
                    PublishYear = 1985,
                    Title = "Pierwsza kniga",
                    Availability = 0,
                    OwnerId = 1,
                },
                new BookEntity
                {
                    Id = 2,
                    Author = "Paweł Jackowski",
                    ISBN = "123-12-34-55678-9",
                    Pages = 523,
                    Publisher = "Moderna",
                    PublishYear = 1993,
                    Title = "Druga kniga",
                    Availability = 2,
                    OwnerId = 2,
                }
            );


            modelBuilder.Entity<OwnerEntity>()
                .OwnsOne(x => x.Address) // Adress is not alone entity, it groups some data. In this case, program will create all columns from Adress in OwnerEntity table.
                .HasData( // Populate adresses of egsisting Owners with Addresses. Give ID consist of base class name (OwnerEntity) and it's PrimaryKey (Id)
                    new { OwnerEntityId = 1, City = "Kraków", Street = "Św. Filipa 17", PostalCode = "31-150", Region = "małopolskie" },
                    new { OwnerEntityId = 2, City = "Kraków", Street = "Krowoderska 45/6", PostalCode = "31-150", Region = "małopolskie" }
                );
        }
    }
}
