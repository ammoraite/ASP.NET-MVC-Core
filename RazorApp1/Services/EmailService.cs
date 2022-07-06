using MimeKit;
using MailKit.Net.Smtp;
using AppInterfases.ServiseIntefaces;
namespace RazorApp1.Services
{
    public class EmailService: IEmailSender
    {
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
                     client.Connect ("smtp.beget.com", 25, false);
                     client.Authenticate ("asp2022gb@rodion-m.ru", "3drtLSa1");
                     client.Send (emailMessage);
                     client.Disconnect (true);
                }
            }
        }
    }

