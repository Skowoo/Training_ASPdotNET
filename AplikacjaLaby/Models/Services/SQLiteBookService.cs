using AplikacjaLaby.Classes;
using AplikacjaLaby.Mappers;
using AplikacjaLabyData;
using AplikacjaLabyData.Entities;

namespace AplikacjaLaby.Models.Services
{
    public class SQLiteBookService : IBookService
    {
        private readonly AppDbContext _context;

        private readonly ITimeProvider _timeProvider;

        public SQLiteBookService(AppDbContext context, ITimeProvider timeProvider)
        {
            _context = context;
            _timeProvider = timeProvider;
        }

        public int Add(Book item)
        {
            item.Id = _context.Books.Max(x => x.Id) + 1;
            item.Created = _timeProvider.GetCurrentTime();
            _context.Add(BookMapper.ToEntity(item));
            _context.SaveChanges();
            return item.Id;
        }

        public void Delete(int id)
        {
            BookEntity? target = _context.Books.Find(id);
            if (target != null)
            {
                _context.Books.Remove(target);
                _context.SaveChanges();
            }                
        }

        public List<Book> GetAll() => _context.Books.Select(x => BookMapper.FromEntity(x)).ToList();

        public Book? GetById(int id)
        {
            BookEntity? target = _context.Books.Find(id);
            if (target != null)
                return BookMapper.FromEntity(target);
            else 
                return null;
        }

        public void Update(Book item) 
        { 
            _context.Books.Update(BookMapper.ToEntity(item)); 
            _context.SaveChanges();
        }

        public List<OwnerEntity> GetAllOwners() => _context.Owners.ToList();

        public PagingList<Book> FindPage(int page, int size) => 
            PagingList<Book>.Create((p, s) 
                => _context.Books
            .OrderBy(b => b.Title)
            .Skip((p - 1) * size).Take(s)
            .Select(x => BookMapper.FromEntity(x))
            .ToList(), 
            _context.Books.Count(), page, size);

    }
}
