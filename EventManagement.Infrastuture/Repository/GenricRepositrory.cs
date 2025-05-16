using EventManagement.Application.Interface;
using EventManagement.Infrastuture.DataBase;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Infrastuture.Repository
{
    public class GenricRepositrory<T>(AppDbContext context) : IGenricRepository<T> where T : class
    {
        private readonly AppDbContext _context = context;
        private readonly DbSet<T> _dbSet = context.Set<T>();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        public async Task DeleteAsync(T entity)
        {
            await Task.FromResult(_dbSet.Remove(entity));
        }
        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult(_dbSet.AsEnumerable());
        }
        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
        public async Task UpdateAsync(T entity)
        {
            await Task.FromResult(_dbSet.Update(entity));
        }
    }
}
