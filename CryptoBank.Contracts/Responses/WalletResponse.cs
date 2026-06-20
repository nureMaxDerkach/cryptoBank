namespace CryptoBank.Contracts.Responses;

public record WalletResponse(
    long Id,
    string Address,
    string CurrencyCode,
    decimal Balance,
    DateTime CreatedAt);