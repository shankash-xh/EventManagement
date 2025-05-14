using Microsoft.AspNetCore.Identity;

namespace EventManagement.Domain.Entity
{
    public class User : IdentityUser
    {
        public string? RefeshToken { get; set; }
        public DateTime? RefeshTokenExpiryTime { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }

}
