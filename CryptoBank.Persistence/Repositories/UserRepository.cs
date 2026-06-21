using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class UserRepository(CryptoBankDbContext dbContext) : IUserRepository
{
    public async Task<User> AddAsync(User user)
    {
        var entityEntry = await dbContext.Users.AddAsync(user);
        return entityEntry.Entity;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await dbContext.Users.Include(x => x.Country).FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<bool> IsEmailUniqueAsync(string email)
    {
        return !await dbContext.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await dbContext.Users.Include(x => x.Country).ToListAsync();
    }
}