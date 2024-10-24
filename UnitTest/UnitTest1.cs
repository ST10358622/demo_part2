using Microsoft.AspNetCore.Mvc;
using System;
using System.Diagnostics;
using UnitTest1.Models;

namespace UnitTest1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
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

        //register function
        [HttpPost]
        public IActionResult register(register get_all)
        {
            string name = get_all.name;
            string email = get_all.email;
            string password = get_all.password;

            //pass all to methods 
            string results = get_all.check_user(name, email, password);

            Console.WriteLine(results);
            return RedirectToAction("Index", "Home");
        }

    }
}