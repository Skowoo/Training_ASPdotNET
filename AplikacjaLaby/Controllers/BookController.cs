using AplikacjaLaby.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaLaby.Controllers
{
    public class BookController : Controller
    {
        // MUST BE STATIC or else it will initialize each time controller is called
        static List<Book> _books = new() { new Book { Id = 1, Author = "Janusz Tracz", ISBN = "321-21-21-54321-2", Pages = 200, Publisher = "Rebis", PublishYear = 2000, Title = "Pierwsza"} };

        public IActionResult Index()
        {
            return View(_books);
        }

        [HttpGet]
        public IActionResult Create() //Return form
        {
            return View();
        }

        [HttpPost] // Called from FORM - when posted
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                book.Id = _books.Count == 0 ? 1 : _books.Max(x => x.Id) + 1;
                _books.Add(book);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book); 
            }
        }

        public IActionResult Delete(int id)
        {
            var target = _books.Where(x => x.Id == id).Single();
            _books.Remove(target);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            var target = _books.Where(x => x.Id == id).Single();
            return View(target);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var target = _books.Where(x => x.Id == id).Single();
            return View(target);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var target = _books.Where(x => x.Id == book.Id).Single();

                target.Author = book.Author;
                target.Title = book.Title;
                target.ISBN = book.ISBN;
                target.Publisher = book.Publisher;
                target.PublishYear = book.PublishYear;
                target.Pages = book.Pages;

                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
    }
}
