using AppInterfases.ServiseIntefaces;

using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;
using RazorApp1.Services;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        private static ProductCatalog _catalog = new ( );
        IEmailSender _emailSender;
        public CatalogController ( IEmailSender emailSender )
        {
            _emailSender=emailSender;
        }

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
            _catalog.AddProductInCatalog (product);
            _emailSender.SendEmail ("valera.koltirin@yandex.ru", "AddNewProduct",
                $"добавлен новый продукт ID:{product.ProductId} Name:{product.ProductName} Prise:{product.Prise}");
            return View ( );
        }
    }
}
