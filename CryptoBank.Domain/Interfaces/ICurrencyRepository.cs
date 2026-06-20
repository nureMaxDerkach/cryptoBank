using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface ICurrencyRepository
{
    Task<Currency?> GetByIdAsync(long id);
}