using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Bookings.Command;

public class UpdateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<UpdateBookingRequest, Result<BookingResponce>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<BookingResponce>> Handle(UpdateBookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        if (bookingRequest == null)
        {
            return Result<BookingResponce>.Failure("Invalid Booking Data");
        }
        Booking booking = _mapper.Map<Booking>(bookingRequest);
        await _unitOfWork.Bookings.UpdateAsync(booking);
        await _unitOfWork.SaveAsync();
        BookingResponce? bookingResponse = _mapper.Map<BookingResponce>(booking);
        return Result<BookingResponce>.Success(bookingResponse);
    }
}

