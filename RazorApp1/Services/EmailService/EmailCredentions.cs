namespace RazorApp1.Services.EmailService
{
    public class EmailCredentions
    {
        public string EmailFrom { get; set; }
        public string EmailTo { get; set; }
        public string NameOfSender { get; set; }
        public int ReTryCount { get; set; } = 1;

    }
}
