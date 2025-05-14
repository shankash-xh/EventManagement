using EventManagement.Domain.Entity;

namespace EventManagement.Application.Interface;

public interface IUserRepository : IGenricRepository<User>
{
    Task<User?> GetByEmailAsync(string email);
}
