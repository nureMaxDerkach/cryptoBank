using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface ITransactionRepository
{
    Task<Transaction> AddAsync(Transaction transaction);
    Task<List<Transaction>> GetAllByUserIdAsync(long userId);
}