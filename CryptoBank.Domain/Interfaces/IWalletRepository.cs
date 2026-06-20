using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface IWalletRepository
{
    Task<Wallet> AddAsync(Wallet wallet);
    
    Task<List<Wallet>> GetAllBuUserIdAsync(long userId);
    
    Task<Wallet?> GetByIdAsync(long id);
    
    Task<Wallet?> GetByAddressAsync(string address);
}