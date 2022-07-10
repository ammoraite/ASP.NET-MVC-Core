using System.Collections.Concurrent;

using Interfases;
using Interfases.CatalogInterfaces;

namespace RazorApp1.Models
{
    public class ProductCatalog : IproductCatalog
    {
        private ConcurrentDictionary<int, IProduct> Products = new ( );


        public bool ContainsProductInCatalog ( IProduct product )
        {
           
                foreach (var item in Products)
                {
                    if (item.Value.Equals (product))
                    {
                        return true;
                    }
                }
                return false;

        }

        public bool AddProductInCatalog ( IProduct product )
        {
           return Products.TryAdd (product.ProductId, product);
        }

        public bool RemoveProductInCatalog ( IProduct product )
        {
            return Products.TryRemove (product.ProductId, out _);
        }
        public IEnumerable<IProduct> GetProductsInCatalog ( )
        {
            foreach (var item in Products)
            {
                yield return item.Value;
            }
        }
    }
}
