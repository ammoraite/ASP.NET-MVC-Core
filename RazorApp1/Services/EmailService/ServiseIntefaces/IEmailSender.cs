namespace RazorApp1.Services.EmailService.ServiseIntefaces
{
    public interface IEmailSender: IAsyncDisposable
    {
        public Task SendAsync ( string email, string subject, string message, CancellationToken cancellationToken );
        public Task SendBegetEmailPoliticAsync ( string email, string subject, string message , CancellationToken cancellationToken );
    }
}
