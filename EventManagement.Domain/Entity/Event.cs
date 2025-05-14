using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Domain.Entity
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string OrganizerId { get; set; }
        public string Location { get; set; }
        public DateTime Time { get; set; }
        public int Capacity { get; set; }
        public bool IsPrivate { get; set; }

        public ICollection<Booking> Bookings { get; set; } = [];
    }
}
