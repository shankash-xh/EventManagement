using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Request.Booking;

public class GetAllBookingsRequest : IRequest<Result<List<BookingResponce>>>
{
    public string? FilterOn { get; set; }
    public string? FilterQuery { get; set; }
    public string? SortOn { get; set; }
    public bool IsAscending { get; set; }
    public int PageNumber { get; set; } = 1;
    public int PageSize { get; set; } = 5;
}
