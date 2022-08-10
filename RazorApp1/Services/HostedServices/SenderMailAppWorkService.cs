using System.Diagnostics;

using EmailSenderWebApi.Domain.DomainEvents.EventConsumers;

using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Services.HostedServices
{
    public class SenderMailAppWorkService : IHostedService
    {
        private readonly ILogger<SenderMailAppWorkService> _logger;
        private IEmailSender _emailSender { get; set; }
        public SenderMailAppWorkService ( ILogger<SenderMailAppWorkService> logger, IEmailSender emailSender )
        {
            _logger=logger??throw new NullReferenceException (nameof (logger));
            _emailSender=emailSender??throw new NullReferenceException (nameof (logger));

        }
        public Task StartAsync ( CancellationToken stoppingToken )
        {
            Task.Run (async ( ) =>
             {
                 using var timer = new PeriodicTimer (TimeSpan.FromHours (1));
                 Stopwatch sw = Stopwatch.StartNew ( );
                 while (await timer.WaitForNextTickAsync (stoppingToken))
                 {
                     sw.Start ( );

                     await _emailSender.SendEmailWithPoliticAsync (
                           "valera.koltirin@yandex.ru",
                           "Работа сервера",
                           "сервер работает исправно",
                           stoppingToken);

                     sw.Stop ( );

                     TimeSpan ts = sw.Elapsed;

                     string elapsedTime = string.Format ("{0:00}:{1:00}:{2:00}.{3:00}",
                            ts.Hours, ts.Minutes, ts.Seconds,
                            ts.Milliseconds/10);

                     _logger.LogInformation ("сообщение о работе сервера отправлено за {elapsedTime}", elapsedTime);
                 }
             });
            _logger.LogInformation ("Хост {Type} запущен успешно", typeof (SenderMailAppWorkService).Name);
            return Task.CompletedTask;
        }

        public async Task StopAsync ( CancellationToken cancellationToken )
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await _emailSender.DisposeAsync ( );
                _logger.LogInformation ("Хост {Type} завершил свою работу", typeof (SenderMailAppWorkService).Name);
            }
        }
    }
}
