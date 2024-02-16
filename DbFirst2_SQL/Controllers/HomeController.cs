using DbFirst2_SQL.Models;
using DbFirst2_SQL.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DbFirst2_SQL.Controllers
{
    public class HomeController : Controller
    {
        private static IService _service;
        public HomeController(IService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.Products);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
