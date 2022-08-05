
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;

using RazorApp1.Models.Entityes;

namespace EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog
{
    public class ProductAddedEvent : ProductChangedEvent
    {
        public ProductAddedEvent ( Product product, CancellationToken cancellationToken )
            : base (product, cancellationToken)
        {
                EmailMessage=$"Добавлен Продукт\n"+
                $"ID: {product.ProductId}\n"+
                $"Name: {product.ProductName}\n"+
                $"Prise: {product.Prise}";
        }
    }
}
