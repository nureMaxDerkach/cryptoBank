using CryptoBank.Domain.Enums;

namespace CryptoBank.Contracts.Responses;

public record TransactionResponse(
    long Id,
    TransactionType Type,
    TransactionStatus Status,
    string FromCurrencyCode,
    decimal FromAmount,
    string ToCurrencyCode,
    decimal ToAmount,
    bool IsExternal,
    string? RecipientReference,
    DateTime CreatedAt);