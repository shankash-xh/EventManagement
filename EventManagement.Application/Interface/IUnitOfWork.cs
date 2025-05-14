
namespace EventManagement.Application.Interface;

public interface IUnitOfWork
{
    IBookingRepository Bookings { get; }
    IEventRepository Events { get; }
    IUserRepository Users { get; }
    Task SaveAsync();
}
