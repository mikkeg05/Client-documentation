using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace ClientDocumentation.Web.Business.Services
{
    public static class MailService
    {
        public static void SendMail(string body)
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = true;
            client.PickupDirectoryLocation = @"C:\Users\mikkel.gunge\OneDrive\Documents\Projects\Client-documentation\ClientDocumentation.Web\ClientDocumentation.Web\Test\";
            client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            var emailMessage = BuildMailMessage("test", body, "mikkel.gunge@akqa.com", "mikkel.gunge@akqa.com");
            emailMessage.IsBodyHtml = true;
            client.Send(emailMessage);            
        }

        public static MailMessage BuildMailMessage(string sub, string body, string to, string from)
        {
            return new MailMessage(from, to)
            {
                Subject = sub,
                Body = body,

            };
        }
    }
}