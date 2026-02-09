using MailKit.Net.Smtp;
using MimeKit;
using System.Threading.Tasks;

namespace EmailProject.Services
{
    public class EmailService
    {
        public async Task SendConfirmCodeAsync(string email, string code)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress("IdentityAdmin", "yilmaziyigun1@gmail.com"));
            mimeMessage.To.Add(new MailboxAddress("User", email));
            mimeMessage.Subject = "Email Doğrulama Kodunuz";
            mimeMessage.Body = new TextPart("plain") { Text = $"Doğrulama Kodunuz: {code}" };

            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync("smtp.gmail.com", 587, false);
            await smtpClient.AuthenticateAsync("yilmaziyigun1@gmail.com", "xoym jbrf lskc fphd");
            await smtpClient.SendAsync(mimeMessage);
            await smtpClient.DisconnectAsync(true);
        }
    }
}
