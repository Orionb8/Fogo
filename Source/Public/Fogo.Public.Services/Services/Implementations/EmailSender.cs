using Fogo.Configuration;

using Microsoft.Extensions.Options;

using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Fogo.Services.Implementations {

    public class EmailSender : IEmailSender {
        private readonly EmailSettings _settings;

        public EmailSender(IOptions<EmailSettings> settings) {
            _settings = settings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage) {
            using MailMessage message = new MailMessage(_settings.From, email, subject, htmlMessage) {
                IsBodyHtml = true,
            };
            using SmtpClient smtpClient = new SmtpClient(_settings.Host, _settings.Port) {
                EnableSsl = true,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(_settings.From, _settings.Password)
            };
            await smtpClient.SendMailAsync(message);
        }
    }
}