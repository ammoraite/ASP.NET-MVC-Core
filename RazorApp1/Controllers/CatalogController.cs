
using EmailSenderWebApi.Domain.DomainEvents.EventConsumers;
using EmailSenderWebApi.Models.EmailModels;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;
using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        public static ProductCatalog _catalog = new ( );
        private readonly ILogger<CatalogController> _logger;

        public CatalogController ( ILogger<CatalogController> logger)
        {
            _logger=logger??throw new ArgumentNullException (nameof (logger));
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
        public async Task<IActionResult> AddProduct ( Product product, CancellationToken cancellationToken )
        {
            try
            {
                await _catalog.AddProductInCatalog (product, cancellationToken);
            }
            catch (Exception e)
            {
                _logger.LogWarning (e, "");
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
            }
            catch (Exception e)
            {
                _logger.LogWarning (e, "");
            }
            return View ( );
        }
    }
}
