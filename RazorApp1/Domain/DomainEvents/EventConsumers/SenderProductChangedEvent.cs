
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog;
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;
using EmailSenderWebApi.DomainEvents;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts;

using Microsoft.Extensions.Options;

using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace EmailSenderWebApi.Domain.DomainEvents.EventConsumers
{
    public class SenderProductChangedEvent : ISenderProductChangedEvent
    {
        private readonly IEmailSender _emailSender;
        private readonly IOptions<OptionsEmailSender> _options;
        public SenderProductChangedEvent ( IEmailSender emailSender ,IOptions<OptionsEmailSender> options)
        {
            _options=options??throw new ArgumentNullException (nameof (options));
            _emailSender=emailSender??throw new ArgumentNullException (nameof (emailSender));
            DomainEventsManager.Register<ProductAddedEvent> (e => SendEmailNotification (e,_options.Value.OptionsAddProduct));
            DomainEventsManager.Register<ProductRemovedEvent> (e => SendEmailNotification (e, _options.Value.OptionsRemoveProduct));
        }
        public Task SendEmailNotification ( IProductChangedEvent e, IEmailOptions options )
        {
            var task = _emailSender.SendBegetEmailPoliticAsync (
            options.Mail,
            options.Subject,
            e.EmailMessage,
            e.CancellationToken);
            task.Start ( );
            return task;
        }
    }
}
