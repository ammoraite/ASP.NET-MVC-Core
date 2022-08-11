namespace EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items.Abstracts
{
    public interface IEmailOptions
    {
        public string? Subject { get; set; }
        public string? Mail { get; set; }
    }
}