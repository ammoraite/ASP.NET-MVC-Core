using EmailSenderWebApi.Models.EmailModels;

using Microsoft.Extensions.Options;

using MimeKit;

using Polly;
using Polly.Retry;

using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Services.EmailService
{
    public class EmailSenderService : IEmailSender
    {
        private readonly ILogger<EmailSenderService>? _logger;
        private readonly SmtpCredentions? _smtpCredentions;
        private readonly EmailCredentions? _emailCredentions;
        private MailKit.Net.Smtp.SmtpClient? _smtpClient;
        private readonly object _locker = new object ( );

        public EmailSenderService (
            IOptions<EmailCredentions> emailoptions,
            IOptions<SmtpCredentions> smtpoptions,
            ILogger<EmailSenderService> logger )
        {
            _logger=logger??throw new ArgumentNullException (nameof (logger));
            _smtpCredentions=smtpoptions.Value??throw new ArgumentNullException (nameof (smtpoptions));
            _emailCredentions=emailoptions.Value??throw new ArgumentNullException (nameof (emailoptions));
        }

        public async ValueTask DisposeAsync ( )
        {
            if (_smtpClient is not null)
            {
                if (_smtpClient.IsConnected)
                {
#pragma warning disable U2U1016 // Use a CancellationToken when possible
                    await _smtpClient.DisconnectAsync (true);
#pragma warning restore U2U1016 // Use a CancellationToken when possible
                }
                _smtpClient.Dispose ( );
            }
        }

        private async Task Send ( string email, string subject, string message, CancellationToken cancellationToken )
        {
            _smtpClient=new ( );
            try
            {
                MimeMessage emailMessage = new ( );
                if (_emailCredentions is not null)
                {
                    emailMessage.From.Add (new MailboxAddress (_emailCredentions.NameOfSender, _emailCredentions.EmailFrom));
                    emailMessage.To.Add (new MailboxAddress (_emailCredentions.EmailTo, email));
                }
                else
                {
                    throw new NullReferenceException (nameof (_emailCredentions));
                }

                emailMessage.Subject=subject;
                emailMessage.Body=new TextPart (MimeKit.Text.TextFormat.Html)
                {
                    Text=message
                };

                if (_smtpCredentions is not null)
                {
                    await _smtpClient.ConnectAsync (_smtpCredentions.Host, 25, false, cancellationToken);
                    await _smtpClient.AuthenticateAsync (_smtpCredentions.UserName, _smtpCredentions.Password, cancellationToken);
                }
                else
                {
                    throw new NullReferenceException (nameof (_smtpCredentions));
                }
                await _smtpClient.SendAsync (emailMessage, cancellationToken);
                await _smtpClient.DisconnectAsync (true, cancellationToken);

                if (_logger is not null)
                {
                    var Email = _emailCredentions.EmailTo;
                    _logger.LogInformation ("Успешно отправлено сообщение на {Email}", email);
                }
            }
            catch (Exception ex)
            {
                if (_logger is not null&&_emailCredentions is not null)
                {
                    var Email = _emailCredentions.EmailTo;
                    var mes = ex.Message;
                    _logger.LogWarning (ex, "Не удалось отправить сообщение на {Email}"+
                    " так как {mes}", email, mes);
                }
                else if (_logger is not null)
                {
                    var mes = ex.Message;
                    _logger.LogWarning (ex, "Не удалось отправить сообщение так как {mes}", mes);
                }
            }

        }
        public async Task SendBegetEmailPoliticAsync ( string email, string subject, string message, CancellationToken cancellationToken )
        {
            if (_emailCredentions is not null)
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
                token => Send (email, subject, message, token), cancellationToken);


                if (result.Outcome==OutcomeType.Failure&&_logger is not null)
                {
                    _logger.LogError (result.FinalException, "There was an error while sending email");
                }
            }
            else
            {
                throw new NullReferenceException (nameof (_emailCredentions));
            }

        }
    }
}

