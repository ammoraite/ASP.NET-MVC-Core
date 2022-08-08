
using EmailSenderWebApi.DomainEvents;

using RazorApp1.Models.Entityes;

namespace EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass
{
    public interface IProductChangedEvent : IDomainEvent
    {
        public Product Product { get; }
        public CancellationToken CancellationToken { get; }
        public string EmailMessage { get; }
    }
}
