using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts;

using Microsoft.Extensions.Options;

namespace EmailSenderWebApi.Domain.DomainEvents.EventConsumers
{
    public interface ISenderProductChangedEvent
    {
        public Task SendEmailNotification ( IProductChangedEvent e, IEmailOptions options);
    }
}