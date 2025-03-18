using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace PlaywrightScraper.Notifier
{
    public class EmailNotifier
    {
        private string email = "your_email@gmail.com";
        private string password = "your_password";
        private string recipMail = "recipient_email@gmail.com";
        public void SendEmail(string subject, string body)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com") { 
                Port = 587,
                Credentials = new NetworkCredential(email, password),
                EnableSsl = true,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(email),
                Subject = subject,
                Body = body,
                IsBodyHtml = false,
            };

            mailMessage.To.Add(recipMail);

            smtpClient.Send(mailMessage);
        }
    }
}
