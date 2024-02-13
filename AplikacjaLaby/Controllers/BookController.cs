using AplikacjaLaby.Models;
using AplikacjaLaby.Models.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AplikacjaLaby.Controllers
{
    [Authorize(Roles = "Admin")]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View(_bookService.GetAll());
        }

        [HttpGet]
        public IActionResult Create() //Return form
        {
            Book newBook = new();

            newBook.Owners =
                _bookService
                .GetAllOwners()
                .Select(x => new SelectListItem() { Text = $"{x.Name} {x.Surname}", Value = x.Id.ToString() }) // Transform Owner into SelectListItem using LINQ
                .ToList();

            return View(newBook);
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
                book.Owners =
                    _bookService
                    .GetAllOwners()
                    .Select(x => new SelectListItem() { Text = $"{x.Name} {x.Surname}", Value = x.Id.ToString() }) // Transform Owner into SelectListItem using LINQ
                    .ToList();

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

            if (target is null)
                RedirectToAction("Index");

            target!.Owners = 
                _bookService.GetAllOwners()
                .Select(x => new SelectListItem() { Text = $"{x.Name} {x.Surname}", Value = x.Id.ToString() })
                .ToList();

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
                book.Owners =
                    _bookService.GetAllOwners()
                    .Select(x => new SelectListItem() { Text = $"{x.Name} {x.Surname}", Value = x.Id.ToString() })
                    .ToList();

                return View(book);
            }
        }
    }
}
