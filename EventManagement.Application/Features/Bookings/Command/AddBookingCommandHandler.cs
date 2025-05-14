using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Responce;
using EventManagement.Domain.Entity;
using EventManagement.Shared.GlobalResponce;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventManagement.Application.Features.Bookings.Command;

public class AddBookingCommandHandler : IRequestHandler<AddBookingRequest, Result<BookingResponce>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AddBookingCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<BookingResponce>> Handle(AddBookingRequest booking, CancellationToken cancellationToken)
    {
        if (booking == null)
        {
            return Result<BookingResponce>.Failure("Invalid Booking Data");
        }
        var k = _mapper.Map<Booking>(booking);
        await _unitOfWork.Bookings.AddAsync(k);
        await _unitOfWork.SaveAsync();
        var bookingResponse = _mapper.Map<BookingResponce>(k);
        return Result<BookingResponce>.Success(bookingResponse);
    }
}
