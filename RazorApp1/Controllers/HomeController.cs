using System.Diagnostics;

using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;

namespace RazorApp1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController ( ILogger<HomeController> logger )
        {
            _logger=logger;
            _logger.LogInformation ("Запуск HomeController");
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
        }
    }
}