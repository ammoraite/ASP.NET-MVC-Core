using Microsoft.AspNetCore.Mvc;
using RazorApp1.Models;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new();

        [HttpGet]
        public IActionResult Categories()
        {
            return View(_catalog);
        }

        [HttpPost]
        public IActionResult Categories(Catergory model)
        {
            _catalog.Catergories.Add(model);
            return View(_catalog);
        }
    }
}
