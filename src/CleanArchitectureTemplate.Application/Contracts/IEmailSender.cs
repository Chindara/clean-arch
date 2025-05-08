using CleanArchitectureTemplate.Application.DTO;

namespace CleanArchitectureTemplate.Application.Contracts;

public interface IEmailSender
{
    Task<bool> SendEmailAsync(EmailMessage emailMessage);

    Task<bool> SendEmailAsync(EmailMessage emailMessage, Dictionary<string, byte[]> attachments);
}