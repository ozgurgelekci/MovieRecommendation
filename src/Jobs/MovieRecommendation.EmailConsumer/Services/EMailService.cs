using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using MovieRecommendation.Domain.Models.Settings;
using MovieRecommendation.EmailConsumer.Services.Abstract;
using MovieRecommendation.Infrastructure.Models;

namespace MovieRecommendation.EmailConsumer.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        private readonly ILogger<EmailService> _logger;

        public EmailService(IOptions<MailSettings> mailSettings, ILogger<EmailService> logger)
        {
            _mailSettings = mailSettings.Value;
            _logger = logger;
        }

        public async Task SendAsync(EMailRecommendationModel request)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mailSettings.EmailFrom);
                email.To.Add(MailboxAddress.Parse(request.ReceiveEmail));
                email.Subject = _mailSettings.Subject;
                var builder = new BodyBuilder();
                builder.HtmlBody = $"New movie recommendation from {request.SenderName} : {request.MovieName}";
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch
            {
            }
        }
    }
}
