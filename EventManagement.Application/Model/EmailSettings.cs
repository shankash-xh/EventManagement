namespace EventManagement.Application.Model;

public class EmailSettings
{
    public string? Client { get; set; }
    public int Port { get; set; }
    public string? FromAddress { get; set; }
    public string? FromName { get; set; }
    public string? AppKey { get; set; }
}
