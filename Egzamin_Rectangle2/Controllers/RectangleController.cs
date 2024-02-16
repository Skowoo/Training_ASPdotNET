using Egzamin_Rectangle2.Models;
using Egzamin_Rectangle2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Egzamin_Rectangle2.Controllers
{
    public class RectangleController : Controller
    {
        private static IRectangleService _rectangleService;

        public RectangleController(IRectangleService rs)
        {
            _rectangleService = rs;
        }

        public IActionResult Index()
        {
            return View(_rectangleService.GetAll());
        }

        [HttpGet]
        public IActionResult Create() => View();

        [HttpPost]
        public IActionResult Create(Rectangle input)
        {
            if (ModelState.IsValid)
            {
                int newId = _rectangleService.Add(input);
                // Redircet to action with parameter - new Id taken from Service method
                return RedirectToAction("Details", new { id = newId });
            }

            return View(input);
        }

        public IActionResult Details (int id)
        {
            var target = _rectangleService.GetById(id);

            if (target is null)
            {
                return NotFound();
            }

            return View(target);
        }
    }
}
