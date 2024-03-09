namespace CleanSample.Domain.Miscellaneous;
public interface IEmailSender
{
    Task SendEmailAsync(string fromAddress, string toAddress, string subject, string body, IEnumerable<string>? cc = null, IEnumerable<string>? bcc = null, bool isBodyHtml = false);
}
