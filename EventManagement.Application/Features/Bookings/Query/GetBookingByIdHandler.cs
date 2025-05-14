using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Bookings.Query;

public class GetBookingByIdHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetBookingByIdRequest, Result<BookingResponce>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BookingResponce>> Handle(GetBookingByIdRequest request, CancellationToken cancellationToken)
    {
        Booking? booking = await _unitOfWork.Bookings.GetByIdAsync(request.Id);
        if (booking != null)
        {
            var bookingResponse = _mapper.Map<BookingResponce>(booking);
            return Result<BookingResponce>.Success(bookingResponse);
        }
        return Result<BookingResponce>.Failure("Booking Not Found");
    }
}

