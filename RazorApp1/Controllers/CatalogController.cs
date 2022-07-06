using Interfases.CatalogInterfaces;

using Microsoft.AspNetCore.Mvc;
using RazorApp1.Models;
using RazorApp1.Models.Entityes;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        private static ProductCatalog _catalog = new();

        [HttpGet]
        public IActionResult Products()
        {
            return View(_catalog);
        }

        [HttpGet]
        public IActionResult AddProduct ( )
        {     
            return View ();
        }

        [HttpPost]
        public IActionResult AddProduct ( Product product)
        {
            _catalog.AddProductInCatalog (product);
            return View();
        }
    }
}
