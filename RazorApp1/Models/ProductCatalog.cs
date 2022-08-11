using System.Collections.Concurrent;

using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog;
using EmailSenderWebApi.DomainEvents;

using RazorApp1.Models.Entityes;

namespace RazorApp1.Models
{
    public class ProductCatalog : IProductCatalog
    {
        private readonly ConcurrentDictionary<int, Product> Products = new ( );




        //public bool ContainsProductInCatalog ( Product product )
        //{

        //    foreach (var item in Products)
        //    {
        //        if (item.Value.Equals (product))
        //        {
        //            return true;
        //        }
        //    }
        //    return false;

        //}

        public Task AddProductInCatalog ( Product product, CancellationToken cancellationToken )
        {
            DomainEventsManager.Raise (new ProductAddedEvent (product, cancellationToken));
            Products.TryAdd (product.ProductId, product);

            return Task.CompletedTask;
        }

        public Task RemoveProductInCatalog ( Product product, CancellationToken cancellationToken )
        {

            DomainEventsManager.Raise (new ProductRemovedEvent (product, cancellationToken));
            Products.TryRemove (product.ProductId, out _);

            return Task.CompletedTask;
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
