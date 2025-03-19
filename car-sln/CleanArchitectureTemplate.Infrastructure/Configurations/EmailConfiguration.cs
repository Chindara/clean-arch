namespace CleanArchitectureTemplate.Infrastructure.Configurations;

public class EmailConfiguration
{
    public string FromAddress { get; set; }
    public string FromName { get; set; }
    public string SmtpServer { get; set; }
    public int SmtpPort { get; set; }
    public string SmtpUsername { get; set; }
    public string SmtpPassword { get; set; }
}