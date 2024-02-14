using AplikacjaLaby.Classes;
using AplikacjaLabyData.Entities;

namespace AplikacjaLaby.Models.Services
{
    public class MemoryBookService : IBookService
    {
        private readonly ITimeProvider _timeProvider;

        public MemoryBookService(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        static readonly List<Book> _books =
        [new Book
        {
            Id = 1,
            Author = "Janusz Tracz",
            ISBN = "321-21-21-54321-2",
            Pages = 200,
            Publisher = "Rebis",
            PublishYear = 2000,
            Title = "Pierwsza",
            Availability = Availability.High
        }];

        public int Add(Book item)
        {
            item.Id = _books.Count == 0 ? 1 : _books.Max(x => x.Id) + 1;
            item.Created = _timeProvider.GetCurrentTime();
            _books.Add(item);
            return item.Id;
        }

        public void Delete(int id)
        {
            var target = _books.Where(x => x.Id == id).SingleOrDefault();

            if (target != null)
                _books.Remove(target);
        }

        public List<Book> GetAll() => _books;

        public Book? GetById(int id) => _books.Where(x => x.Id == id).SingleOrDefault();

        public void Update(Book item)
        {
            var target = _books.Where(x => x.Id == item.Id).Single();

            target.Author = item.Author;
            target.Title = item.Title;
            target.ISBN = item.ISBN;
            target.Publisher = item.Publisher;
            target.PublishYear = item.PublishYear;
            target.Pages = item.Pages;
            target.Availability = item.Availability;
        }

        public List<OwnerEntity> GetAllOwners() => throw new NotImplementedException();

        public PagingList<Book> FindPage(int page, int size) => throw new NotImplementedException();

    }
}
