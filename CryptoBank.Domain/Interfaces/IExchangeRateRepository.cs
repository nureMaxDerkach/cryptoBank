using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface IExchangeRateRepository
{
    Task<ExchangeRate?> GetByCurrencyIdAsync(long currencyId);

    Task<List<ExchangeRate>> GetAllAsync();
}