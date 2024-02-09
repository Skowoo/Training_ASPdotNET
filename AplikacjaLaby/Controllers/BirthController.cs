using AplikacjaLaby.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaLaby.Controllers
{
    public class BirthController : Controller
    {
        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result([FromForm]Birth model)
        {
            if (model.IsValid())
                return View(model);
            else
                return View();
        }
    }
}
