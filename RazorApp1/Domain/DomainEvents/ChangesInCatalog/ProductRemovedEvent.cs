
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;

using RazorApp1.Models.Entityes;

namespace EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog
{
    public class ProductRemovedEvent : ProductChangedEvent
    {
        public ProductRemovedEvent ( Product product, CancellationToken cancellationToken )
            : base (product, cancellationToken)
        {
            EmailMessage=
            $"Удален продукт \n"+
            $"ID:{product.ProductId} \n"+
            $"Name:{product.ProductName} \n"+
            $"Prise:{product.Prise}";
        }
    }
}
