using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bukvoyizhka.Core.Sender
{
    public class GmailSmtpClient : SmtpClient
    {
        
        public GmailSmtpClient()
        {           
            Port = 587;
            Host = "smtp.gmail.com";
            EnableSsl = true;
            UseDefaultCredentials = false;           
            DeliveryMethod  = SmtpDeliveryMethod.Network;            
        }

        
    }
}
