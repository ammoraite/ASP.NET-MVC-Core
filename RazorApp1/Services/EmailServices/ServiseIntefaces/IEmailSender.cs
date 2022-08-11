namespace RazorApp1.Services.EmailService.ServiseIntefaces
{
    public interface IEmailSender : IAsyncDisposable
    {
        public Task SendEmailWithPoliticAsync ( string email, string subject, string message, CancellationToken cancellationToken );
    }
}
