
using Microsoft.AspNetCore.Mvc;

using RazorApp1.Models;
using RazorApp1.Models.Entityes;
using RazorApp1.Models.Entityes.ProductEntityes;

namespace RazorApp1.Controllers
{
    public class CatalogController : Controller
    {
        public static ProductCatalog _catalog = new ( );

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
        public IActionResult AddProduct
        (
            int ProductCatergoryId,
            string ProductCatergoryName,
            int ProductId,
            string ProductName,
            string prise
            )

        {
            new Task (( ) =>
            {
                decimal Prise = Convert.ToDecimal (prise);
                ProductCategory productCategory = new ProductCategory
                {
                    ProductCatergoryName=ProductCatergoryName,
                    ProductCatergoryId=ProductCatergoryId
                };
                Product prod = new Product
                {
                    ProductId=ProductId,
                    ProductName=ProductName,
                    Prise=Prise
                };

                if (_catalog.ContainsCategory (productCategory))
                {
                    foreach (var category in _catalog.Catergories.Where (x => _catalog.ContainsCategory (productCategory)))
                    {
                        if (category.ContainsProduct (prod))
                        {
                            foreach (var product in category.Products.Where (x => category.ContainsProduct (prod)))
                            {
                                product.Prise=prod.Prise;
                            }
                        }
                        else
                        {
                            category.Products.Add (prod);
                        }
                    }
                }
                else
                {
                    productCategory.Products.Add (prod);
                    _catalog.Catergories.Add (productCategory);
                }
            }).Start ( );

            return View ( );
        }
    }
}
