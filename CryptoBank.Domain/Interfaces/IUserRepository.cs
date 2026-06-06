using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> AddAsync(User user);
    
    Task<User?> GetByEmailAsync(string email);
    
    Task<bool> IsEmailUniqueAsync(string email);
}