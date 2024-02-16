using DbFirst1.Models;
using DbFirst1.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DbFirst1.Controllers
{
    public class HomeController : Controller
    {
        private static IProductService ProductService;
        public HomeController(IProductService ps)
        {
            ProductService = ps;
        }

        public IActionResult Index()
        {
            return View(ProductService.Products);
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
