namespace CryptoBank.Contracts.Requests;

public record SendCryptoRequest(long FromWalletId, string RecipientAddress, decimal Amount);