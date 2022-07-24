namespace RazorApp1.Services.EmailService.ServiseIntefaces
{
    public interface IEmailSender
    {
        public EmailCredentions _emailCredentions { get; set; }
        public Task SendAsync ( string email, string subject, string message);
        public Task SendBegetEmailPoliticAsync ( string email, string subject, string message );
    }
}
