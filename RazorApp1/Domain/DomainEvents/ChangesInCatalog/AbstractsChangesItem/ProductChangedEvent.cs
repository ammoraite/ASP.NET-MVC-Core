using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;

using RazorApp1.Models.Entityes;

namespace EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass
{
    public class ProductChangedEvent : IProductChangedEvent
    {
        public Product Product { get; }
        public CancellationToken CancellationToken { get; }
        public string EmailMessage { get; set; }

        public ProductChangedEvent ( Product product, CancellationToken cancellationToken )
        {
            Product=product??throw new ArgumentNullException (nameof(product) );
            CancellationToken=cancellationToken;
        }
    }
}
