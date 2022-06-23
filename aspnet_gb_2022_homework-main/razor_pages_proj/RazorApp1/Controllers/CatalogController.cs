using Microsoft.AspNetCore.Mvc;
using RazorApp1.Models;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        private static Catalog _catalog = new();
        
        [HttpGet]
        public IActionResult Products( )
        {
            return View(_catalog);
        }

        [HttpGet]
        public IActionResult Categories ( )
        {
            return View ();
        }

        [HttpPost]
        public IActionResult Categories(Product productModel,Catergory categoryModel )
        {
            categoryModel.Products.Add(new Product()
            {
                ProductId = productModel.ProductId,
                ProductName = productModel.ProductName
            });
            _catalog.Catergories.Add(new Catergory()
            {
                CatergoryId = categoryModel.CatergoryId,
                CatergoryName = categoryModel.CatergoryName,
                Products = categoryModel.Products
            });
            return View ();
        }
    }
}
