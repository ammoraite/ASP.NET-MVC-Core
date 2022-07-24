using MimeKit;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using RazorApp1.Services.EmailService.ServiseIntefaces;
using Polly.Retry;
using Polly;

namespace RazorApp1.Services.EmailService
{
    public class BegetEmailSenderService : IEmailSender
    {
        private readonly ILogger<BegetEmailSenderService> _logger;
        private readonly SmtpCredentions _smtpCredentions;
        public EmailCredentions _emailCredentions { get; set; }
        public BegetEmailSenderService ( IOptions<EmailCredentions> Emailoptions, IOptions<SmtpCredentions> Smtpoptions, ILogger<BegetEmailSenderService> logger )
        {
            try
            {
                _logger=logger;
                _smtpCredentions=Smtpoptions.Value;
                _emailCredentions=Emailoptions.Value;
            }
            catch (Exception e)
            {
                _logger.LogWarning (e,$"Не удалось инициализировать BegetEmailSenderService так как {e.Message}");  
            }
           
            _logger.LogInformation ("Инициализирован BegetEmailSenderService");
            
        }
        
        public async Task SendAsync ( string email, string subject, string message)
        {
            await Task.Run (( ) =>
            {
                try
                {
                    var emailMessage = new MimeMessage ( );

                    emailMessage.From.Add (new MailboxAddress (_emailCredentions.NameOfSender, _emailCredentions.EmailFrom));
                    emailMessage.To.Add (new MailboxAddress (_emailCredentions.EmailTo, email));
                    emailMessage.Subject=subject;
                    emailMessage.Body=new TextPart (MimeKit.Text.TextFormat.Html)
                    {
                        Text=message
                    };

                    using (var client = new SmtpClient ( ))
                    {


                        client.Connect (_smtpCredentions.Host, 25, false);
                        client.Authenticate (_smtpCredentions.UserName, _smtpCredentions.Password);
                        client.Send (emailMessage);
                        client.Disconnect (true);
                        _logger.LogInformation ($"Успешно отправлено сообщение на {_emailCredentions.EmailTo}");




                    }
                }
                catch (Exception e)
                {

                    _logger.LogWarning (e, $"Не удалось отправить сообщение на {_emailCredentions.EmailTo}"+
                        $" так как {e.Message}");
                   
                }
            });
        }
        public async Task SendBegetEmailPoliticAsync ( string email, string subject, string message )
        {
            AsyncRetryPolicy? policy = Policy
            .Handle<Exception> ( )
            .RetryAsync (_emailCredentions.ReTryCount, onRetry: ( exception, retryAttempt ) =>
            {
                _logger.LogWarning (
                exception, "Error while sending email. Retrying: {Attempt}", retryAttempt);
            });

            PolicyResult? result = await policy.ExecuteAndCaptureAsync (
                                       ( ) => SendAsync (email, subject, message));

            if (result.Outcome==OutcomeType.Failure)
            {
                _logger.LogError (result.FinalException, "There was an error while sending email");
            }

        }
    }
}

