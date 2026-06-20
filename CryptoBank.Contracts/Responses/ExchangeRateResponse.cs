namespace CryptoBank.Contracts.Responses;

public record ExchangeRateResponse(string CurrencyCode, decimal RateToUsd, DateTime UpdatedAt);