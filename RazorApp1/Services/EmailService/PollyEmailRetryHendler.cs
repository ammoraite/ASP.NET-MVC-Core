using Polly;
using Polly.Retry;
using RazorApp1.Services.EmailService.ServiseIntefaces;

namespace RazorApp1.Services.EmailService
{
    public class PollyEmailRetryHendler
    {
        private readonly ILogger<IEmailSender> _logger;
        private readonly IEmailSender _emailSender;

        public PollyEmailRetryHendler ( ILogger<IEmailSender> logger, IEmailSender emailSender )
        {
            _logger=logger;
            _emailSender=emailSender;

        }
        public async Task SendBegetEmailPoliticAsync ( string email, string subject, string message )
        {
            AsyncRetryPolicy? policy = Policy
            .Handle<Exception> ( )
            .RetryAsync (_emailSender._emailCredentions.ReTryCount, onRetry: ( exception, retryAttempt ) =>
        {
            _logger.LogWarning (
            exception, "Error while sending email. Retrying: {Attempt}", retryAttempt);
        });

            PolicyResult? result = await policy.ExecuteAndCaptureAsync (
                                       ( ) => _emailSender.SendAsync (email, subject, message));

            if (result.Outcome==OutcomeType.Failure)
            {
                _logger.LogError (result.FinalException, "There was an error while sending email");
            }

        }
    }
}
