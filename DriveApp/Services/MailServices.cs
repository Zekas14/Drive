using DriveApp.Services.Interfaces;
using DriveApp.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace DriveApp.Services
{
    public class MailServices(IOptions<MailSetting> mailSetting) : IMailServices
    {
        private readonly MailSetting _mailSetting = mailSetting.Value;

        public async Task SendEmailAsync(string mailTo, string subject, string body, IList<IFormFile>? attachment = null)
        {
            var email = new MimeMessage
            {
                Sender = MailboxAddress.Parse(_mailSetting.Email),
                Subject = subject,

            };
            email.To.Add(MailboxAddress.Parse(mailTo));
            var builder = new BodyBuilder();
            if (attachment != null)
            {
                byte[] fileBytes;
                foreach (var item in attachment)
                {
                    if (item.Length > 0)
                    {
                        using var ms = new MemoryStream();
                        item.CopyTo(ms);
                        fileBytes = ms.ToArray();
                        builder.Attachments.Add(item.FileName, fileBytes, ContentType.Parse(item.ContentType));
                    }
                }
            }
            builder.HtmlBody=body;
            email.Body = builder.ToMessageBody();
            email.From.Add(new MailboxAddress(_mailSetting.SenderName,_mailSetting.Email));
            using var smtp = new SmtpClient();
            smtp.Connect(_mailSetting.Server,_mailSetting.Port,MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate(_mailSetting.Email, _mailSetting.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

    }
}