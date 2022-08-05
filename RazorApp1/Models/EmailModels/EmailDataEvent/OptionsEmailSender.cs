using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EmailSenderWebApi.Models.EmailModels.EmailDataEvent.Items;

using Microsoft.Extensions.Options;

namespace EmailSenderWebApi.Models.EmailModels.EmailDataEvent
{
    public class OptionsEmailSender 
    {
        public EmailOptionsAddProduct OptionsAddProduct { get; set; }

        public EmailOptionsRemoveProduct OptionsRemoveProduct { get; set; }
    }
}
