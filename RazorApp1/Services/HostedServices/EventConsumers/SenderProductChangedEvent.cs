
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog;
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;
using EmailSenderWebApi.DomainEvents;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts;

using Microsoft.Extensions.Options;

using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace EmailSenderWebApi.Domain.DomainEvents.EventConsumers
{
    public class SenderProductChangedEvent : ISenderProductChangedEvent, IHostedService
    {
        private readonly IEmailSender _emailSender;
        private readonly IOptions<OptionsEmailSender> _options;
        public SenderProductChangedEvent ( IEmailSender emailSender ,IOptions<OptionsEmailSender> options)
        {
            _options=options??throw new ArgumentNullException (nameof (options));
            _emailSender=emailSender??throw new ArgumentNullException (nameof (emailSender));
            DomainEventsManager.Register<ProductAddedEvent> ( async e =>await SendEmailNotification (e,_options.Value.OptionsAddProduct));
            DomainEventsManager.Register<ProductRemovedEvent> ( async e =>await  SendEmailNotification (e, _options.Value.OptionsRemoveProduct));
        }
        public async Task SendEmailNotification ( IProductChangedEvent e, IEmailOptions options )
        {
            _ = options.Mail??throw new NullReferenceException ( );
            _=options.Subject??throw new NullReferenceException ( );
            await _emailSender.SendBegetEmailPoliticAsync (
            options.Mail,
            options.Subject,
            e.EmailMessage,
            e.CancellationToken);
        }

        public Task StartAsync ( CancellationToken cancellationToken )
        {
           return Task.CompletedTask;
        }

        public async Task StopAsync ( CancellationToken cancellationToken )
        {
           await _emailSender.DisposeAsync ( ); 
        }
    }
}
