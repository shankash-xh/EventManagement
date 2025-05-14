using AutoMapper;
using EventManagement.Application.Interface;
using EventManagement.Application.Request.Booking;
using EventManagement.Application.Responce;
using EventManagement.Shared.GlobalResponce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController(IUnitOfWork unitOfWork, IMapper mapper) : BaseController
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IMapper _mapper = mapper;

        [HttpGet("get-all-bookings")]
        [Authorize(Roles = "Organizers,User")]
        public async Task<Result<List<BookingResponce>>> GetAllBookings([FromQuery] GetAllBookingsRequest bookingRequest)
        {
            Result<List<BookingResponce>>? result = await Mediator.Send(bookingRequest);
            return result;
        }
        [HttpGet("get-booking-by-id")]
        [Authorize(Roles = "Organizers,User")]
        public async Task<Result<BookingResponce>> GetBookingById(GetBookingByIdRequest request)
        {
            Result<BookingResponce>? result = await Mediator.Send(request);
            return result;
        }
        [HttpPost("create-booking")]
        [Authorize(Roles = "Organizers")]
        public async Task<Result<BookingResponce>> CreateBooking([FromBody] AddBookingRequest booking)
        {
            Result<BookingResponce>? result = await Mediator.Send(booking);
            return result;
        }
        [HttpPut("update-booking")]
        [Authorize(Roles = "Organizers")]
        public async Task<Result<BookingResponce>> UpdateBooking(UpdateBookingRequest bookingRequest)
        {
            Result<BookingResponce>? result = await Mediator.Send(bookingRequest);
            return result;
        }
        [HttpDelete("delete-booking")]
        [Authorize(Roles = "Organizers")]
        public async Task<Result<string>> DeleteBooking(DeleteBookingRequest deleteRequest)
        {
            var result = await Mediator.Send(deleteRequest);
            return result ? Result<string>.Success("Deleted") : Result<string>.Failure("Failed to Delete");
        }
    }
}
