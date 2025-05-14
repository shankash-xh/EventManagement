using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Request.Booking;

public class GetBookingByIdRequest : IRequest<Result<BookingResponce>>
{
    public int Id { get; set; }
}
