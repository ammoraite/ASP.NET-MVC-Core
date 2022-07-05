<<<<<<< HEAD
﻿using Microsoft.AspNetCore.Mvc;
using RazorApp1.Models;
using System.Diagnostics;
=======
﻿using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
>>>>>>> Update_Collection

namespace RazorApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

<<<<<<< HEAD
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewData["Footer"] = "(c) GeekBrains";
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
=======
        public HomeController ( ILogger<HomeController> logger )
        {
            _logger=logger;
        }

        public IActionResult Index ( )
        {
            ViewData["Footer"]="(c) GeekBrains";
            return View ( );
        }

        public IActionResult Privacy ( )
        {
            return View ( );
        }

        [ResponseCache (Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error ( )
        {
            return View (new ErrorViewModel { RequestId=Activity.Current?.Id??HttpContext.TraceIdentifier });
>>>>>>> Update_Collection
        }
    }
}