
using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;
using RazorApp1.Services.EmailService;
using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        private static readonly ProductCatalog _catalog = new ( );
        private IEmailSender _emailSender;
        private ILogger<IEmailSender> _logger;
        public CatalogController ( IEmailSender emailSender, ILogger<IEmailSender> logger )
        {
            _logger=logger??throw new ArgumentNullException(nameof(logger));
            _emailSender=emailSender??throw new ArgumentNullException (nameof (emailSender));
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

                if (product is not null&&_catalog.AddProductInCatalog (product))
                {
                    await Task.Run(()=> _emailSender.SendBegetEmailPoliticAsync (
                    "valera.koltirin@yandex.ru",
                    "AddNewProduct",
                    $"добавлен новый продукт ID:{product.ProductId} Name:{product.ProductName} Prise:{product.Prise}"));
                }
                else if(product is null)
                {
                    throw new NullReferenceException (nameof (product));
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
