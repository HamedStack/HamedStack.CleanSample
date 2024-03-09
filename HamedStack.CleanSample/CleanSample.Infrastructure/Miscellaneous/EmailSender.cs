// ReSharper disable UnusedMember.Global

using CleanSample.Domain.Miscellaneous;
using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

namespace CleanSample.Infrastructure.Miscellaneous; 

public class EmailSender(string smtpServer, int smtpPort, string smtpUsername, string smtpPassword)
    : IEmailSender
{
    public async Task SendEmailAsync(string fromAddress, string toAddress, string subject, string body, IEnumerable<string>? cc = null, IEnumerable<string>? bcc = null, bool isBodyHtml = false)
    {
        var emailMessage = new MimeMessage();

        emailMessage.From.Add(MailboxAddress.Parse(fromAddress));
        emailMessage.To.Add(MailboxAddress.Parse(toAddress));

        if (cc != null)
        {
            foreach (var ccAddress in cc)
            {
                emailMessage.Cc.Add(MailboxAddress.Parse(ccAddress));
            }
        }

        if (bcc != null)
        {
            foreach (var bccAddress in bcc)
            {
                emailMessage.Bcc.Add(MailboxAddress.Parse(bccAddress));
            }
        }

        emailMessage.Subject = subject;

        emailMessage.Body = isBodyHtml 
            ? new TextPart(MimeKit.Text.TextFormat.Html) { Text = body } 
            : new TextPart(MimeKit.Text.TextFormat.Plain) { Text = body };

        using var client = new SmtpClient();
        
        // For demo purposes, accept all SSL certificates (in production, use a valid certificate)
        client.ServerCertificateValidationCallback = (s, c, h, e) => true;

        await client.ConnectAsync(smtpServer, smtpPort, SecureSocketOptions.StartTls).ConfigureAwait(false);
        await client.AuthenticateAsync(smtpUsername, smtpPassword).ConfigureAwait(false);
        await client.SendAsync(emailMessage).ConfigureAwait(false);
        await client.DisconnectAsync(true).ConfigureAwait(false);
    }
}