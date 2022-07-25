using MailKit.Net.Smtp;

using Microsoft.Extensions.Options;

using MimeKit;

using Polly;
using Polly.Retry;

using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Services.EmailService
{
    public class BegetEmailSenderService : IEmailSender
    {
        private readonly ILogger<BegetEmailSenderService>? _logger;
        private readonly SmtpCredentions? _smtpCredentions;
        public EmailCredentions? _emailCredentions;
        public BegetEmailSenderService ( IOptions<EmailCredentions> Emailoptions, IOptions<SmtpCredentions> Smtpoptions, ILogger<BegetEmailSenderService> logger )
        {
            try
            {
                _logger=logger??throw new ArgumentNullException (nameof (logger));
                _smtpCredentions=Smtpoptions.Value??throw new ArgumentNullException (nameof (Smtpoptions));
                _emailCredentions=Emailoptions.Value??throw new ArgumentNullException (nameof (Emailoptions));
            }
            catch (Exception e)
            {
                if (_logger is not null)
                {
                    _logger.LogWarning (e,$"Не удалось инициализировать BegetEmailSenderService так как {e.Message}");
                }
            }
            if (_logger is not null)
            {
                _logger.LogInformation ("Инициализирован BegetEmailSenderService");
            }
        }

        public async Task SendAsync ( string email, string subject, string message )
        {
            await Task.Run (( ) =>
            {
                try
                {
                    if (_emailCredentions is not null)
                    {
                        if (_smtpCredentions is not null)
                        {
                            MimeMessage emailMessage = new ( );
                            emailMessage.From.Add (new MailboxAddress (_emailCredentions.NameOfSender, _emailCredentions.EmailFrom));
                            emailMessage.To.Add (new MailboxAddress (_emailCredentions.EmailTo, email));
                            emailMessage.Subject=subject;
                            emailMessage.Body=new TextPart (MimeKit.Text.TextFormat.Html)
                            {
                                Text=message
                            };
                            using SmtpClient? client = new ( );
                            client.Connect (_smtpCredentions.Host, 25, false);
                            client.Authenticate (_smtpCredentions.UserName, _smtpCredentions.Password);
                            client.Send (emailMessage);
                            client.Disconnect (true);
                            if (_logger is not null)
                            {
                                _logger.LogInformation ($"Успешно отправлено сообщение на {_emailCredentions.EmailTo}");
                            }
                        }
                        else
                        {
                            throw new NullReferenceException (nameof (_smtpCredentions));
                        }
                    }
                    else
                    {
                        throw new NullReferenceException (nameof (_emailCredentions));
                    }
                }
                catch (Exception e)
                {
                    if (_logger is not null&&_emailCredentions is not null)
                    {
                        _logger.LogWarning (e, $"Не удалось отправить сообщение на {_emailCredentions.EmailTo}"+
                         $" так как {e.Message}");
                    }
                    else if (_logger is not null)
                    {
                        _logger.LogWarning (e, $"Не удалось отправить сообщение так как {e.Message}");
                    }
                }
            });
        }
        public async Task SendBegetEmailPoliticAsync ( string email, string subject, string message )
        {

            if (_emailCredentions is not null)
            {
                if (_smtpCredentions is not null)
                {
                    AsyncRetryPolicy? policy = Policy
                    .Handle<Exception> ( )
                    .RetryAsync (_emailCredentions.ReTryCount, onRetry: ( exception, retryAttempt ) =>
                    {
                        if (_logger is not null)
                        {
                            _logger.LogWarning (exception, "Error while sending email. Retrying: {Attempt}", retryAttempt);
                        }
                    });

                    PolicyResult? result = await policy.ExecuteAndCaptureAsync (
                                               ( ) => SendAsync (email, subject, message));
                    if (result.Outcome==OutcomeType.Failure&&_logger is not null)
                    {
                        _logger.LogError (result.FinalException, "There was an error while sending email");
                    }
                }
                else
                {
                    throw new NullReferenceException (nameof (_smtpCredentions));
                }
            }
            else
            {
                throw new NullReferenceException (nameof (_emailCredentions));
            }
        }
    }
}

