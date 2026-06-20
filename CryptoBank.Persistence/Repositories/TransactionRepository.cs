using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class TransactionRepository(CryptoBankDbContext dbContext) : ITransactionRepository
{
    public async Task<Transaction> AddAsync(Transaction transaction)
    {
        var entry = await dbContext.Transactions.AddAsync(transaction);
        return entry.Entity;
    }

    public async Task<List<Transaction>> GetAllByUserIdAsync(long userId)
    {
        return await dbContext.Transactions
            .Include(t => t.FromCurrency)
            .Include(t => t.ToCurrency)
            .Where(t => t.UserId == userId)
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}