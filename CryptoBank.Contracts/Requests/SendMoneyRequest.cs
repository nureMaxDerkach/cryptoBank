namespace CryptoBank.Contracts.Requests;

public record SendMoneyRequest(long FromCardId, string RecipientCardNumber, decimal Amount);