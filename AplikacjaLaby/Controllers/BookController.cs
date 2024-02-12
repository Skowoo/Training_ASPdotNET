using AplikacjaLaby.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaLaby.Controllers
{
    public class BookController : Controller
    {
        // Moved to SERVICE!
        //// MUST BE STATIC or else it will initialize each time controller is called
        //static readonly List<Book> _books =
        //[new Book { 
        //    Id = 1, 
        //    Author = "Janusz Tracz", 
        //    ISBN = "321-21-21-54321-2",
        //    Pages = 200, Publisher = "Rebis", 
        //    PublishYear = 2000, 
        //    Title = "Pierwsza", 
        //    Availability = Availability.High 
        //} ]; 

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        public IActionResult Index()
        {
            return View(_bookService.GetAll());
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
                _bookService.Add(book);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book); 
            }
        }

        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Details(int? id)
        {
            var target = _bookService.GetById((int)id!);
            return View(target);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var target = _bookService.GetById((int)id!);
            return View(target);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _bookService.Update(book);
                return RedirectToAction("Index");
            }
            else
            {
                return View(book);
            }
        }
    }
}
