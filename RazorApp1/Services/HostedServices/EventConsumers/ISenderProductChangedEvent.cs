using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts;

namespace EmailSenderWebApi.Domain.DomainEvents.EventConsumers
{
    public interface ISenderProductChangedEvent
    {
        public Task SendEmailAboutChanges ( IProductChangedEvent e, IEmailOptions options );
    }
}