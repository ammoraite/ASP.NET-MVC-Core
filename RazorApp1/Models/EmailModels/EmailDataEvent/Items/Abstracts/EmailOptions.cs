namespace EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts
{
    public class EmailOptions : IEmailOptions
    {
        public string? Subject { get; set; }
        public string? Mail { get; set; }
    }
}
