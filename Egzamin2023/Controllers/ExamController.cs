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

        public ExamController(IDateProvider dateProvider, NoteService noteService)
        {
            _dateProvider = dateProvider;
            _noteService = noteService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Note note)
        {
            // Add ModelError to given atribute with given message string - must be done before validation
            if (note.Deadline < _dateProvider.CurrentDate.AddHours(1)) 
                ModelState.AddModelError("Deadline", "Czas ważności musi być o godzinę późniejszy od bieżącego czasu!");

            if (ModelState.IsValid)
            {
                _noteService.Add(note);

                return RedirectToAction("Index");
            }
            
            return View(note);
        }

        public IActionResult Index() => View(_noteService.GetAll());

        // Remember about null case
        public IActionResult Details(string id) 
        {
            // Take item from Service (Service must return nullable prop)
            var target = _noteService.GetById(id);

            // if target is null (not found in service)
            if (target is null) 
                return BadRequest(); // Return BadRequest() Status: 400

            // If everything is OK - return view with target
            return View(target);
        }
    }
}
