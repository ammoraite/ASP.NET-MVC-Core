using System.Collections.Concurrent;

using EmailSenderWebApi.Services.CountersServices.ClickCouterService;

using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {

        private readonly ILogger<CatalogController> _logger;
        private readonly IClickCounter clickCounter;
        private static ProductCatalog _catalog = new ( );
        public static ConcurrentDictionary<string, int> PathClickCount;
        public CatalogController ( ILogger<CatalogController> logger, ILogger<ProductCatalog> loggerCat, IClickCounter clickCounter)
        {
            _logger=logger??throw new ArgumentNullException (nameof (logger));
            this.clickCounter=clickCounter;
        }

        [HttpGet]
        public IActionResult Products ( )
        {
            return View (_catalog);
        }

        [HttpGet]
        public IActionResult Metrics ( )
        {
            return View (clickCounter);
        }

        [HttpGet]
        public IActionResult AddProduct ( )
        {
            return View ( );
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct ( Product product, CancellationToken cancellationToken )
        {
            try
            {
                await _catalog.AddProductInCatalog (product, cancellationToken);
                _logger.LogInformation ("Добавлен товар в каталог {product}",
                    product.ProductName);
            }
            catch (Exception e)
            {
                _logger.LogWarning (e, "Не удалось добавить товар в каталог {product}",
                    nameof (product));
            }
            return View ( );
        }

        [HttpGet]
        public IActionResult DeleteProduct ( )
        {
            return View ( );
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct ( Product product, CancellationToken cancellationToken )
        {
            try
            {
                await _catalog.RemoveProductInCatalog (product, cancellationToken);
                _logger.LogInformation ("Товар {product} удален из каталога", product.ProductName);

            }
            catch (Exception e)
            {
                _logger.LogWarning (e, "Не удалось удалить товар {product} в каталоге ", product.ProductName);

            }
            return View ( );
        }
    }
}
