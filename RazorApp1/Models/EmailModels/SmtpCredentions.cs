namespace EmailSenderWebApi.Models.EmailModels
{
    public class SmtpCredentions
    {
        public string Host { get; set; } = "";
        public string UserName { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
