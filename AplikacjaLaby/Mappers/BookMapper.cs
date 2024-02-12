using AplikacjaLaby.Models;
using AplikacjaLabyData.Entities;

namespace AplikacjaLaby.Mappers
{
    public class BookMapper
    {
        public static Book FromEntity(BookEntity input)
        {
            Book result = new();
            result.Id = input.Id;
            result.Title = input.Title;
            result.Author = input.Author;
            result.Pages = input.Pages;
            result.ISBN = input.ISBN;
            result.PublishYear = input.PublishYear;
            result.Publisher = input.Publisher;
            result.Availability = (Availability)input.Availability;
            result.Created = input.Created;

            return result;
        }

        public static BookEntity ToEntity(Book input) 
        { 
            BookEntity result = new();

            result.Id = input.Id;
            result.Title = input.Title;
            result.Author = input.Author;
            result.Pages = input.Pages;
            result.ISBN = input.ISBN;
            result.PublishYear = input.PublishYear;
            result.Publisher = input.Publisher;
            result.Availability = (int)input.Availability;
            result.Created = input.Created;

            return result;
        }
    }
}
