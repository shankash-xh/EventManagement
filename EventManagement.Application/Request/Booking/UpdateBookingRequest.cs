using EventManagement.Application.Responce;
using EventManagement.Shared.Enums;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Request.Booking;

public class UpdateBookingRequest : IRequest<Result<BookingResponce>>
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int EventId { get; set; }
    public DateTime CreatedAt { get; set; }
    public StatusEnum Status { get; set; }
}
