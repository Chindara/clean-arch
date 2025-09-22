using CleanArchitectureTemplate.Application.Contracts;
using CleanArchitectureTemplate.Application.DTO;
using CleanArchitectureTemplate.Infrastructure.Configurations;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace CleanArchitectureTemplate.Infrastructure;

public class SmtpEmailSender(IOptions<EmailConfiguration> emailConfiguration) : IEmailSender
{
    private readonly EmailConfiguration _emailConfiguration = emailConfiguration.Value;

    public Task<bool> SendEmailAsync(EmailMessage emailMessage)
    {
        var email = new MimeMessage();

        email.From.Add(new MailboxAddress(_emailConfiguration.FromName, _emailConfiguration.FromAddress));
        email.To.Add(new MailboxAddress(emailMessage.ToName, emailMessage.ToEmail));

        email.Headers.Add("X-MT-Category", emailMessage.Category);
        email.Subject = emailMessage.Subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Html)
        {
            Text = emailMessage.Body
        };

        using (var smtp = new SmtpClient())
        {
            smtp.Connect(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);
            smtp.Authenticate(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
            smtp.Send(email);
            smtp.Disconnect(true);
        }

        return Task.FromResult(true);
    }

    public async Task<bool> SendEmailAsync(EmailMessage emailMessage, Dictionary<string, byte[]> attachments)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress(_emailConfiguration.FromName, _emailConfiguration.FromAddress));
        email.To.Add(new MailboxAddress(emailMessage.ToName, emailMessage.ToEmail));
        email.Headers.Add("X-MT-Category", emailMessage.Category);
        email.Subject = emailMessage.Subject;
        var builder = new BodyBuilder
        {
            HtmlBody = emailMessage.Body
        };

        foreach (var attachment in attachments)
        {
            // Get the file extension from the attachment name
            string fileName = attachment.Key;
            string fileExtension = Path.GetExtension(fileName).TrimStart('.').ToLowerInvariant();

            // Determine the MIME type based on file extension
            ContentType contentType = GetContentType(fileExtension);

            // Add the attachment with the appropriate content type
            using (var stream = new MemoryStream(attachment.Value))
            {
                builder.Attachments.Add(fileName, stream.ToArray(), contentType);
            }
        }

        email.Body = builder.ToMessageBody();

        try
        {
            using (var smtp = new SmtpClient())
            {
                await smtp.ConnectAsync(_emailConfiguration.SmtpServer, _emailConfiguration.SmtpPort, false);
                await smtp.AuthenticateAsync(_emailConfiguration.SmtpUsername, _emailConfiguration.SmtpPassword);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);
                return true;
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            Console.WriteLine($"Failed to send email: {ex.Message}");
            return false;
        }
    }

    private ContentType GetContentType(string fileExtension)
    {
        // Map common file extensions to MIME types
        switch (fileExtension)
        {
            case "pdf":
                return new ContentType("application", "pdf");
            case "doc":
            case "docx":
                return new ContentType("application", "msword");
            case "xls":
            case "xlsx":
                return new ContentType("application", "vnd.ms-excel");
            case "ppt":
            case "pptx":
                return new ContentType("application", "vnd.ms-powerpoint");
            case "jpg":
            case "jpeg":
                return new ContentType("image", "jpeg");
            case "png":
                return new ContentType("image", "png");
            case "gif":
                return new ContentType("image", "gif");
            case "txt":
                return new ContentType("text", "plain");
            case "html":
            case "htm":
                return new ContentType("text", "html");
            case "zip":
                return new ContentType("application", "zip");
            case "csv":
                return new ContentType("text", "csv");
            case "json":
                return new ContentType("application", "json");
            case "xml":
                return new ContentType("application", "xml");
            default:
                // Default to application/octet-stream for unknown types
                return new ContentType("application", "octet-stream");
        }
    }
}