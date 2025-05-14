using EventManagement.Application.Interface;
using EventManagement.Domain.Entity;
using EventManagement.Infrastuture.DataBase;

namespace EventManagement.Infrastuture.Repository;

public class BookingRepository(AppDbContext context) : GenricRepositrory<Booking>(context), IBookingRepository
{
}
