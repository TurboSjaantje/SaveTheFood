using Microsoft.AspNetCore.Mvc;
using Portal.Models;
using System.Diagnostics;

namespace Portal.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Login");
        }

        public ViewResult Home()
        {
            return View("Index");
        }

        public ViewResult Meal()
        {
            return View("Meal");
        }
    }
}