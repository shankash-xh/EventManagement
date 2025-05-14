using EventManagement.Application.Interface;
using EventManagement.Infrastuture.DataBase;

namespace EventManagement.Infrastuture.Repository;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext _context = context;
    private IBookingRepository? _bookingRepository;
    private IEventRepository? _eventRepository;
    private IUserRepository? _userRepository;

    public IBookingRepository Bookings
    {
        get
        {
            _bookingRepository ??= new BookingRepository(_context);
            return _bookingRepository;
        }
    }
    public IEventRepository Events
    {
        get
        {
            _eventRepository ??= new EventRepository(_context);
            return _eventRepository;
        }
    }
    public IUserRepository Users
    {
        get
        {
            _userRepository ??= new UserRepository(_context);
            return _userRepository;
        }
    }
    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }


    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
