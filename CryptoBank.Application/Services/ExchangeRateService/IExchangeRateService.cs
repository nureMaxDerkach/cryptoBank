using CryptoBank.Contracts.Responses;

namespace CryptoBank.Application.Services.ExchangeRateService;

public interface IExchangeRateService
{
    Task<decimal> ConvertAsync(long fromCurrency, long toCurrencyId, decimal amount);
    
    Task<List<ExchangeRateResponse>> GetAllRatesAsync();
}