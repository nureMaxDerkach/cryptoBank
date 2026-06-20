using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Interfaces;

namespace CryptoBank.Application.Services.ExchangeRateService;

public class ExchangeRateService(IExchangeRateRepository exchangeRateRepository) : IExchangeRateService
{
    public async Task<decimal> ConvertAsync(long fromCurrencyId, long toCurrencyId, decimal amount)
    {
        if (fromCurrencyId == toCurrencyId)
        {
            return amount;
        }

        var fromRate = await exchangeRateRepository.GetByCurrencyIdAsync(fromCurrencyId);

        if (fromRate is null)
        { 
            throw new Exception($"No exchange rate found for currency {fromCurrencyId}");
        }

        var toRate = await exchangeRateRepository.GetByCurrencyIdAsync(toCurrencyId);

        if (toRate is null)
        {
            throw new Exception($"No exchange rate found for currency {toCurrencyId}");
        }

        var amountInUsd = amount * fromRate.RateToUsd;
        var convertedAmount = amountInUsd / toRate.RateToUsd;

        return convertedAmount;
    }

    public async Task<List<ExchangeRateResponse>> GetAllRatesAsync()
    {
        var rates = await exchangeRateRepository.GetAllAsync();

        return rates.Select(r => new ExchangeRateResponse(
            r.Currency.Code,
            r.RateToUsd,
            r.UpdatedAt)).ToList();
    }
}