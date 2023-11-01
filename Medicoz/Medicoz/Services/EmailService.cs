using MimeKit;
using MimeKit.Text;
using System.Net.Mail;
using MailKit.Net.Smtp;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;
using MailKit.Security;

namespace Medicoz.Services
{
    public class EmailService : IEmailService
    {
        public void Send(string to, string subject, string html)
        {
            
                // create message
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse("aghabadalov@yandex.com"));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = html };

                // send email
                using var smtp = new SmtpClient();
                smtp.Connect("smtp.yandex.ru",587,SecureSocketOptions.StartTls);
                smtp.Authenticate("aghabadalov@yandex.com","A1g2a3b4" );
                smtp.Send(email);
                smtp.Disconnect(true);
            
        }
    }
}
