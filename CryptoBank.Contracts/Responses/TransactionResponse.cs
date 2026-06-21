using CryptoBank.Domain.Enums;

namespace CryptoBank.Contracts.Responses;

public record TransactionResponse(
    long Id,
    TransactionType Type,
    TransactionStatus Status,
    long FromCurrencyId,
    decimal FromAmount,
    long ToCurrencyId,
    decimal ToAmount,
    bool IsExternal,
    string? RecipientReference,
    DateTime CreatedAt);