using System.Collections.Concurrent;

using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog;
using EmailSenderWebApi.DomainEvents;
using EmailSenderWebApi.Models.EmailModels;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;

using RazorApp1.Models.Entityes;

namespace RazorApp1.Models
{
    public class ProductCatalog : IProductCatalog
    {
        private readonly ConcurrentDictionary<int, Product> Products = new ( );


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

        public Task AddProductInCatalog (Product product, CancellationToken cancellationToken )
        {
            Task task = DomainEventsManager.Raise (new ProductAddedEvent (product, cancellationToken));

            if (Products.TryAdd (product.ProductId, product))
            {
                task.Start ( );
            }
            task.Dispose ( );
            return task;
        }

        public Task RemoveProductInCatalog ( Product product, CancellationToken cancellationToken )
        {
            Task task = DomainEventsManager.Raise (new ProductRemovedEvent (product, cancellationToken));

            if (Products.TryRemove (product.ProductId, out _))
            {
                task.Start ( );
            }
            task.Dispose ( );
            return task;
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
