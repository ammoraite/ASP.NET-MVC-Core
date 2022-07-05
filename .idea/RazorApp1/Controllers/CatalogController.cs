
using Interfases;

using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        public static IproductCatalog _catalog=new ProductCatalog();

        [HttpGet]
        public IActionResult Products ( )
        {
            return View (_catalog);
        }

        [HttpGet]
        public IActionResult AddProduct ( )
        {
            return View ( );
        }

        [HttpPost]
        public IActionResult AddProduct ( Product product )
        {
            new Task (( ) => _catalog.AddCategory (product)).Start ( );
            return View ( );
        }
    }
}
