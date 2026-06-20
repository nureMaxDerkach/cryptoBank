using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class CurrencyRepository(CryptoBankDbContext dbContext) : ICurrencyRepository
{
    public async Task<Currency?> GetByIdAsync(long id)
    {
        return await dbContext.Currencies.FirstOrDefaultAsync(c => c.Id == id);
    }
}