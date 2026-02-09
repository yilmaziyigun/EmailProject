using EmailProject.Dtos;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;

namespace EmailProject.Controllers
{
    public class EmailController : Controller
    {
        public IActionResult SendEmail()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendEmail(MailRequestDto mailRequestDto)
        {
            // Email gönderme işlemi burada gerçekleştirilecek
        MimeMessage mimeMessage = new MimeMessage();
           MailboxAddress mailboxAddress = new MailboxAddress("IdentityAdmin", "yilmaziyigun1@gmail.com");
            mimeMessage.From.Add(mailboxAddress);
            mimeMessage.To.Add(new MailboxAddress("User", mailRequestDto.ReceiverEmail));
            mimeMessage.Subject = mailRequestDto.Subject;
            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = mailRequestDto.MessageDetail;
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Connect("smtp.gmail.com", 587, false);
            smtpClient.Authenticate("yilmaziyigun1@gmail.com", "xoym jbrf lskc fphd");
            smtpClient.Send(mimeMessage);
            smtpClient.Disconnect(true);




            return RedirectToAction("UserList");
        }
    }
}
