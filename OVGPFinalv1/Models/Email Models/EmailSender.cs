using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OVGPFinalv1.Models.Email_Models
{
    public class EmailSender : IEmailSender
    {
        public EmailSender(IOptions<SmptConfig> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public SmptConfig Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Execute(Options, subject, message, email);
        }

        public Task Execute(SmptConfig options, string subject, string message, string email)
        {
            var mimeMessage = new MimeMessage();
            mimeMessage.From.Add(new MailboxAddress(options.from, options.from));
            mimeMessage.To.Add(new MailboxAddress(email, email));
            mimeMessage.Subject = subject;

            mimeMessage.Body = new TextPart("plain")
            {
                Text = message
            };


            SmtpClient client = new SmtpClient();

            // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
            client.ServerCertificateValidationCallback = (s, c, h, e) => true;

            client.Connect(options.server, options.port, false);

            // Note: only needed if the SMTP server requires authentication
            client.Authenticate(options.smtpUsername, options.smtpPassword);

            client.Send(mimeMessage);
            client.Disconnect(true);
            client.Dispose();

            return Task.FromResult(0);
        }
    }
}
