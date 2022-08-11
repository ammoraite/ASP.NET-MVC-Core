using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items;

namespace EmailSenderWebApi.Models.EmailModels.EmailDataEvent
{
    public class OptionsEmailSender
    {
        public EmailOptionsAddProduct OptionsAddProduct { get; set; }

        public EmailOptionsRemoveProduct OptionsRemoveProduct { get; set; }
    }
}
