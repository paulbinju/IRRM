using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;

namespace IRRM.Core
{
    public class Utility
    {
        public void sendemail(string from, string to, string subject, string body) {


 

            var message = new MailMessage();
            message.To.Add(new MailAddress(to));
            message.From = new MailAddress(from);
            message.Subject = subject;
            message.Body = string.Format(body);
            message.IsBodyHtml = true;

            var smtp = new SmtpClient
            {
                Host = "mail.mehospital.com",
                Port = 25,
                //EnableSsl = false,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("irrm@mehospital.com", "irrm@meh123")
            };
            try
            {
                smtp.Send(message);
            }
            catch { }

        }
    }
}