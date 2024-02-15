using Egzamin2023.Models;
using Egzamin2023.Services;
using Microsoft.AspNetCore.Mvc;

// Imie Nazwisko IndexNo

namespace Egzamin2023.Controllers
{
    public class ExamController : Controller
    {
        private readonly IDateProvider _dateProvider;
        private readonly NoteService _noteService;

        public ExamController(IDateProvider dateProvider, NoteService ns)
        {
            _dateProvider = dateProvider;
            _noteService = ns;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            if (note.Deadline < _dateProvider.CurrentDate.AddHours(1)) // Model State BEFORE model validation!!!
                ModelState.AddModelError("Deadline", "Czas ważności musi być o godzinę późniejszy od bieżącego czasu!");

            if (ModelState.IsValid)
            {
                _noteService.Add(note);

                return RedirectToAction("Index");
            }
            else return View(note);
        }

        public IActionResult Index() // Display only notes with valid deadline term!
            => View(_noteService.GetAll().Where(x => x.Deadline > _dateProvider.CurrentDate.AddHours(1)).ToList());

        public IActionResult Details(string id) // Remember about null case = no item in collection - return BadRequest!
        {
            var target = _noteService.GetById(id);

            if (target is null)
                return BadRequest();

            else return View(target);
        }
    }
}
