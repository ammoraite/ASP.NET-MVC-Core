
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog;
using EmailSenderWebApi.Domain.DomainEvents.ChangesInCatalog.AbstractChangesClass;
using EmailSenderWebApi.DomainEvents;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent;
using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts;

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

using RazorApp1.Services.EmailService.ServiseIntefaces;
using RazorApp1.Services.HostedServices;

namespace EmailSenderWebApi.Domain.DomainEvents.EventConsumers
{
    public class SenderProductChangedEvent : ISenderProductChangedEvent, IHostedService
    {
        private readonly ILogger<SenderProductChangedEvent> _logger;
        private readonly IOptions<OptionsEmailSender> _options;
        private IEmailSender _emailSender;
        public SenderProductChangedEvent (ILogger<SenderProductChangedEvent> logger, IOptions<OptionsEmailSender> options, IEmailSender emailSender )
        {
            _logger=logger;
            _options=options??throw new ArgumentNullException (nameof (options));
            _emailSender=emailSender??throw new ArgumentNullException (nameof (emailSender));
        }
        public async Task SendEmailAboutChanges ( IProductChangedEvent e, IEmailOptions options )
        {
            try
            {
                _=options.Mail??throw new ArgumentNullException ( );
                _=options.Subject??throw new ArgumentNullException ( );
                await _emailSender.SendBegetEmailPoliticAsync (
                options.Mail,
                options.Subject,
                e.EmailMessage,
                e.CancellationToken);
            }
            catch (Exception exc)
            {
                _logger.LogError (exc,"Ошибка отправки email связанное с событием {Type} так как {exc}",nameof(e) );
            }
            
        }

        public Task StartAsync ( CancellationToken cancellationToken )
        {
            try
            {             
                DomainEventsManager.Register<ProductAddedEvent> (
                    async pae => await SendEmailAboutChanges (pae, _options.Value.OptionsAddProduct));               

                DomainEventsManager.Register<ProductRemovedEvent> (
                    async pre => await SendEmailAboutChanges (pre,_options.Value.OptionsRemoveProduct));               

                _logger.LogInformation ("Все события связанные с {Type} зарегитрированны",typeof (IProductChangedEvent).Name);
                _logger.LogInformation ("Хост {Type} запущен успешно", typeof (SenderProductChangedEvent).Name);
            }
            catch (Exception e)
            {
                _logger.LogWarning (e,"Не удалось зарегистрировать событие в {Type}", typeof (DomainEventsManager).Name);
            }
            return Task.CompletedTask;

        }

        public async Task StopAsync ( CancellationToken cancellationToken )
        {
            await _emailSender.DisposeAsync ( );
            _logger.LogInformation ("Хост {Type} завершил свою работу", typeof (SenderProductChangedEvent).Name);

        }
    }
}
