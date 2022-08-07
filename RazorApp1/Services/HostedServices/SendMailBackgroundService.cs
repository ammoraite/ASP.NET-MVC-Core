using System.Diagnostics;

using EmailSenderWebApi.Models.EmailModels;

using Microsoft.Extensions.Options;

using RazorApp1.Services.EmailService;

namespace RazorApp1.Services.HostedServices
{
    public class SendMailBackgroundService : IHostedService
    {
        private readonly ILogger<SendMailBackgroundService> _logger;
        private EmailSenderService EmailSender { get; set; }
        public SendMailBackgroundService (
            IOptions<EmailCredentions> Emailoptions,
            IOptions<SmtpCredentions> Smtpoptions,
            ILogger<SendMailBackgroundService> logger,
            ILogger<EmailSenderService> loggerEmailSender )
        {
            _logger=logger??throw new NullReferenceException (nameof (logger));

            // сделал так потому что если инициализировать через di, то в момент отправки собщения
            // о добавлении нового продукта 
            // получаю NullReferenceException в методе _smtpClient.Send (emailMessage,cancellationToken);
            EmailSender=new (Emailoptions, Smtpoptions, loggerEmailSender);
        }
        public  Task StartAsync ( CancellationToken stoppingToken )
        {
               Task.Run ( async ( ) =>
                {
                using var timer = new PeriodicTimer (TimeSpan.FromSeconds (15));
                Stopwatch sw = Stopwatch.StartNew ( );
                while (await timer.WaitForNextTickAsync (stoppingToken))
                {
                    sw.Start ( );

                    await EmailSender.SendBegetEmailPoliticAsync (
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
            return Task.CompletedTask;
        }

        public async Task StopAsync ( CancellationToken cancellationToken )
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await EmailSender.DisposeAsync ( );
            }
        }
    }
}
