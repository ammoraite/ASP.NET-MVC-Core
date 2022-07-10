using MimeKit;
using MailKit.Net.Smtp;
using AppInterfases.ServiseIntefaces;
using Microsoft.Extensions.Options;

namespace RazorApp1.Services.EmailService
{
    public class BegetEmailSenderService : IEmailSender
    {
        public BegetEmailSenderService (IOptions<SmtpCredentions> options )
        {
            _smtpCredentions=options.Value;
        }
        private readonly SmtpCredentions _smtpCredentions;
        public void SendEmail ( string email, string subject, string message )
        {
            var emailMessage = new MimeMessage ( );

            emailMessage.From.Add (new MailboxAddress ("Администрация сайта", "asp2022gb@rodion-m.ru"));
            emailMessage.To.Add (new MailboxAddress ("valera.koltirin@yandex.ru", email));
            emailMessage.Subject=subject;
            emailMessage.Body=new TextPart (MimeKit.Text.TextFormat.Html)
            {
                Text=message
            };

            using (var client = new SmtpClient ( ))
            {
                client.Connect (_smtpCredentions.Host, 25, false);
                client.Authenticate (_smtpCredentions.UserName,_smtpCredentions.Password);
                client.Send (emailMessage);
                client.Disconnect (true);
            }
        }
    }
}

