using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;

namespace EventManagement.Application.Features.Bookings.Command;

public class UpdateBookingCommandHandler : IRequestHandler<UpdateBookingRequest, Result<BookingResponce>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public UpdateBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<BookingResponce>> Handle(UpdateBookingRequest bookingRequest, CancellationToken cancellationToken)
    {
        if (bookingRequest == null)
        {
            return Result<BookingResponce>.Failure("Invalid Booking Data");
        }
        var existingBooking = await _unitOfWork.Bookings.GetByIdAsync(bookingRequest.Id);
        if (existingBooking == null)
        {
            return Result<BookingResponce>.Failure("Booking Not Found");
        }
        Booking booking = _mapper.Map<Booking>(bookingRequest);
        await _unitOfWork.Bookings.UpdateAsync(booking);
        await _unitOfWork.SaveAsync();
        var bookingResponse = _mapper.Map<BookingResponce>(existingBooking);
        return Result<BookingResponce>.Success(bookingResponse);
    }
}

