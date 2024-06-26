﻿using AplikacjaLaby.Models;
using Microsoft.AspNetCore.Mvc;

namespace AplikacjaLaby.Controllers
{
    public class CalculatorController : Controller
    {
        public enum Operation
        {
            Unknown, Add, Sub, Mul, Div
        }

        public IActionResult Form()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Result([FromForm] Calculator model) //Pass the model
        {
            if (!model.IsValid()) // If model is not valid - error
                return View("Error", new ErrorViewModel());

            return View(model); //else display view with model
        }

        //public IActionResult Result(Operation op, string a, string b)
        //{
        //    ViewBag.Operation = op;
        //    bool aParsed = double.TryParse(a, out double aValue);            
        //    ViewBag.a = aValue;
        //    bool bParsed = double.TryParse(b, out double bValue);
        //    ViewBag.b = bValue;
        //    double? result = null;

        //    if(aParsed & bParsed)
        //        switch(op)
        //        {
        //            case Operation.Add:
        //                result = aValue + bValue;
        //                break;
        //            case Operation.Sub:
        //                result = aValue - bValue;
        //                break;
        //            case Operation.Mul:
        //                result = aValue * bValue;
        //                break;
        //            case Operation.Div:
        //                result = aValue / bValue;
        //                break;
        //            default:
        //                break;
        //        }

        //    result ??= double.NaN;
        //    ViewBag.result = result;

        //    return View();
        //}

        public IActionResult Index()
        {
            return View();
        }
    }
}
