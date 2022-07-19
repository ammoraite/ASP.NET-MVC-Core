
using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;
using RazorApp1.Services.EmailService;
using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        private static ProductCatalog _catalog = new ( );
        PollyEmailRetryHendler _pollyEmailRetryHendler;
        IEmailSender _emailSender;
        ILogger _logger;
        public CatalogController ( IEmailSender emailSender, ILogger<IEmailSender> logger )
        {
            _logger=logger;
            _emailSender=emailSender;
            _pollyEmailRetryHendler=new (logger, emailSender);
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
            _pollyEmailRetryHendler.SendBegetEmailPoliticAsync (
                "valera.koltirin@yandex.ru", 
                "AddNewProduct",
                $"добавлен новый продукт ID:{product.ProductId} Name:{product.ProductName} Prise:{product.Prise}");

            return View ( );
        }
    }
}
