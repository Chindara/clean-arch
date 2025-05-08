namespace CleanArchitectureTemplate.Application.DTO;

public class EmailMessage
{
    public string Category { get; set; }
    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}