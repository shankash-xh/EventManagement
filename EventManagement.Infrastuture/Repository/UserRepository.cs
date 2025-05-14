using EventManagement.Application.Interface;
using EventManagement.Domain.Entity;
using EventManagement.Infrastuture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastuture.Repository;

public class UserRepository(AppDbContext context) : GenricRepositrory<User>(context), IUserRepository
{
    private readonly AppDbContext _context = context;

    public Task<User?> GetByEmailAsync(string email)
    {
        return _context.Users.FirstOrDefaultAsync(u => u.Email == email);
    }
}

