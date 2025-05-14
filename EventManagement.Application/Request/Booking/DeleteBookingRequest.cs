using MediatR;

namespace EventManagement.Application.Request.Booking;

public class DeleteBookingRequest : IRequest<bool>
{
    public int Id { get; set; }
}
