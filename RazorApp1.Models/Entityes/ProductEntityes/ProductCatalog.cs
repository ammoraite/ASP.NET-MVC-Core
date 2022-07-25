using System.Collections.Concurrent;
using RazorApp1.Models.Entityes;

namespace RazorApp1.Models
{
    public class ProductCatalog : IProductCatalog
    {
        private ConcurrentDictionary<int, Product> Products = new ( );


        public bool ContainsProductInCatalog ( Product product )
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

        public bool AddProductInCatalog ( Product product, CancellationToken cancellationToken )
        {
           return !(cancellationToken.IsCancellationRequested) && Products.TryAdd (product.ProductId, product);
        }

        public bool RemoveProductInCatalog ( Product product )
        {
            return Products.TryRemove (product.ProductId, out _);
        }
        public IEnumerable<Product> GetProductsInCatalog ( )
        {
            foreach (var item in Products)
            {
                yield return item.Value;
            }
        }
    }
}
