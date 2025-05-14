namespace EventManagement.Application.Responce;

public class EventReposnce
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string OrganizerId { get; set; }
    public string Location { get; set; }
    public DateTime Time { get; set; }
    public int Capacity { get; set; }
    public bool IsPrivate { get; set; }
}
