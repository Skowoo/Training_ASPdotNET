using AplikacjaLaby.Models;
using AplikacjaLaby.Models.Services;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaLaby.Controllers
{
    public class BookController : Controller
    {
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
