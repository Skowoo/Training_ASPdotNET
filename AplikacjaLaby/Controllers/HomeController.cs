using AplikacjaLaby.Classes;
using AplikacjaLaby.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AplikacjaLaby.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public enum Operator
        {
            Unknown, Add, Mul, Sub, Div
        } //Unknown wy³apuje wszystkie wartoœci spoza podanych czterech!

        public IActionResult Calculator(Operator op, string a, string b) //stringi pobierane s¹ z Query (pasek adresu)
        {
            bool aResult = double.TryParse(a, out double aParsed);
            ViewBag.a = aParsed;
            bool bResult = double.TryParse(b, out double bParsed);
            ViewBag.b = bParsed;
            ViewBag.Op = op;
            double? result = null;

            if (aResult && bResult)
            {
                switch (op) 
                {
                    case Operator.Add:
                        result = aParsed + bParsed;
                        break;
                    case Operator.Mul:
                        result = aParsed * bParsed;
                        break;
                    case Operator.Sub:
                        result = aParsed - bParsed;
                        break;
                    case Operator.Div:
                        result = aParsed / bParsed;
                        break;
                    default:
                        break;
                }
            }

            result ??= double.NaN;

            ViewBag.Result = result;

            return View();
        }

        public IActionResult About() => View();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Visit"] = Response.HttpContext.Items[LastVisitCookie.CookieName]; // Take data from Response context (added there by middleware layer)
            return View();
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
