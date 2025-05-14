using EventManagement.Shared.Enums;

namespace EventManagement.Domain.Entity
{
    public class Booking
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int EventId { get; set; } 
        public DateTime CreatedAt { get; set; }
        public StatusEnum Status { get; set; }

        public User User { get; set; } = null!;
        public Event Event { get; set; } = null!;

    }
}
