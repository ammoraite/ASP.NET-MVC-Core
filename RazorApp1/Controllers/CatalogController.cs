
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
        IEmailSender _emailSender;
        ILogger _logger;
        public CatalogController ( IEmailSender emailSender, ILogger<IEmailSender> logger )
        {
            _logger=logger;
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
        public async Task<IActionResult> AddProduct ( Product product )
        {
            try
            {
                if (_catalog.AddProductInCatalog (product))
                {
                    await Task.Run(()=> _emailSender.SendBegetEmailPoliticAsync (
                    "valera.koltirin@yandex.ru",
                    "AddNewProduct",
                    $"добавлен новый продукт ID:{product.ProductId} Name:{product.ProductName} Prise:{product.Prise}"));
                }
            }
            catch (Exception e)
            {
                _logger.LogWarning (e,e.Message);
            }
            return View ( );
        }
    }
}
