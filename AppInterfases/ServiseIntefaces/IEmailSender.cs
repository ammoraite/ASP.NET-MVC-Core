namespace AppInterfases.ServiseIntefaces
{
    public interface IEmailSender
    {
        public void SendEmail ( string email, string subject, string message );
    }
}
