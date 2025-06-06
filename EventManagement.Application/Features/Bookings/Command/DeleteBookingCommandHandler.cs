﻿using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Domain.Entity;
using MediatR;

namespace EventManagement.Application.Features.Bookings.Command;
public class DeleteBookingCommandHandler(IMapper mapper, IUnitOfWork unitOfWork) : IRequestHandler<DeleteBookingRequest, bool>
{
    private readonly IMapper _mapper = mapper;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<bool> Handle(DeleteBookingRequest request, CancellationToken cancellationToken)
    {
        Booking booking = _mapper.Map<Booking>(request);
        await _unitOfWork.Bookings.DeleteAsync(booking);
        await _unitOfWork.SaveAsync();
        return true;
    }
}
