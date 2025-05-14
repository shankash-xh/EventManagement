using EventManagement.Shared.Enums;

namespace EventManagement.Application.Responce;

public class BookingResponce
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int EventId { get; set; }
    public DateTime CreatedAt { get; set; }
    public StatusEnum Status { get; set; }
}
