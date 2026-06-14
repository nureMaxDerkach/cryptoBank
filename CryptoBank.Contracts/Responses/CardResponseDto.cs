namespace CryptoBank.Contracts.Responses;

public sealed record CardResponseDto(long Id, long CurrencyId, string CardNumber, string Cvv, decimal Balance, DateOnly ExpiryDate);