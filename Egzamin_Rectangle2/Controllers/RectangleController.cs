using Egzamin_Rectangle2.Models;
using Egzamin_Rectangle2.Services;
using Microsoft.AspNetCore.Mvc;

namespace Egzamin_Rectangle2.Controllers
{
    public class RectangleController : Controller
    {
        private static RectangleService _rectangleService;

        public RectangleController(RectangleService rs)
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
                _rectangleService.Add(input);
                return RedirectToAction($"Index");
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
