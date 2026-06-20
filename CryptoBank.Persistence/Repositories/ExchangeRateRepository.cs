using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class ExchangeRateRepository(CryptoBankDbContext dbContext) : IExchangeRateRepository
{
    public async Task<ExchangeRate?> GetByCurrencyIdAsync(long currencyId)
    {
        return await dbContext.ExchangeRates.FirstOrDefaultAsync(e => e.CurrencyId == currencyId);
    }

    public async Task<List<ExchangeRate>> GetAllAsync()
    {
        return await dbContext.ExchangeRates.Include(e => e.Currency).ToListAsync();
    }
}