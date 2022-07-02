using Microsoft.AspNetCore.Mvc;
using RazorApp1.Models;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        [HttpGet]
        public IActionResult Categories ( )
        {
            return View (Catalog.Products);
        }

        [HttpGet]
        public IActionResult AddProduct ( Product productModel)
        {
            if (productModel.NameCategory!=null&& 
                productModel.NameProduct!=null&&
                productModel.PriseProduct>=0)
            {
                Catalog.Products.Add (productModel);
            }
            return View();
        }
    }
}
