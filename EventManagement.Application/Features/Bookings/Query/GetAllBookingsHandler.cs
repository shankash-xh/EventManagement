using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Bookings.Query;

public class GetAllBookingsHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllBookingsRequest, Result<List<BookingResponce>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<List<BookingResponce>>> Handle(GetAllBookingsRequest bookingRequest, CancellationToken cancellationToken)
    {
        IEnumerable<Booking>? bookings = await _unitOfWork.Bookings.GetAllAsync();
        string? filterOn = bookingRequest.FilterOn;
        string? filterQuery = bookingRequest.FilterQuery;
        string? sortOn = bookingRequest.SortOn;
        bool isAscending = bookingRequest.IsAscending;
        int pageNumber = bookingRequest.PageNumber;
        int pageSize = bookingRequest.PageSize;
        if (bookings != null)
        {
            // Filtering
            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.UserId!.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("EventId", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.EventId.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.CreatedAt.ToString().Equals(filterQuery));
                }
                if (filterOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = bookings.Where(x => x.Status.ToString().Equals(filterQuery));
                }
            }
            // Sorting
            if (!string.IsNullOrEmpty(sortOn))
            {
                if (sortOn.Equals("UserId", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = isAscending ? bookings.OrderBy(x => x.UserId) : bookings.OrderByDescending(x => x.UserId);
                }
                if (sortOn.Equals("EventId", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = isAscending ? bookings.OrderBy(x => x.EventId) : bookings.OrderByDescending(x => x.EventId);
                }
                if (sortOn.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = isAscending ? bookings.OrderBy(x => x.CreatedAt) : bookings.OrderByDescending(x => x.CreatedAt);
                }
                if (sortOn.Equals("Status", StringComparison.OrdinalIgnoreCase))
                {
                    bookings = isAscending ? bookings.OrderBy(x => x.Status) : bookings.OrderByDescending(x => x.Status);
                }
            }
            // Pagination
            int totalCount = bookings.Count();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            List<Booking> paginatedBookings = [.. bookings.Skip((pageNumber - 1) * pageSize).Take(pageSize)];
            List<BookingResponce> bookingResponses = _mapper.Map<List<BookingResponce>>(paginatedBookings);

            return Result<List<BookingResponce>>.Success(bookingResponses);
        }
        return Result<List<BookingResponce>>.Failure("No Bookings Found");
    }
}
