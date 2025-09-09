using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace KASHOP.PL.Utiles
{
    public class EmailSettings : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var Client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("s12220672@stu.najah.edu", "axzi rtml lski ypnj")
            };
            return Client.SendMailAsync(
                new MailMessage(from: "s12220672@stu.najah.edu", to: email, subject, htmlMessage)
                { IsBodyHtml = true }
                );  

        }
    }
}
