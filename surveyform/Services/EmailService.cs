using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using SurveyApp.Models;
using System.Net.Mail;
using System.Net.Mime;

namespace SurveyApp.Services
{
    public class EmailService : IEmailService
    {
        private readonly GmailSettings _settings;

        public EmailService(IOptions<GmailSettings> settings)
        {
            _settings = settings.Value;
        }

        public async Task SendSurveyEmailAsync(string respondentName, string respondentEmail,
            DateTime submittedAt, byte[] pdfBytes, string pdfFileName)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
            message.To.Add(MailboxAddress.Parse(_settings.ReceiverEmail));
            message.Subject = "New Survey Response Received";

            var builder = new BodyBuilder
            {
                HtmlBody = $@"
                    <h3>New Survey Response Received</h3>
                    <p><b>Respondent Name:</b> {respondentName}</p>
                    <p><b>Respondent Email:</b> {respondentEmail}</p>
                    <p><b>Submitted On:</b> {submittedAt:dddd, dd MMMM yyyy - hh:mm tt}</p>
                    <p>A new survey response has been received. The complete response is attached as a PDF.</p>"
            };
            builder.Attachments.Add(pdfFileName, pdfBytes, new MimeKit.ContentType("application", "pdf"));
            message.Body = builder.ToMessageBody();

            using var client = new MailKit.Net.Smtp.SmtpClient();
            await client.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(_settings.SenderEmail, _settings.AppPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
} 