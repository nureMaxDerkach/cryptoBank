namespace CryptoBank.Contracts.Requests;

public record ConvertRequest(long WalletId, long CardId, decimal Amount, ConversionDirection Direction);

public enum ConversionDirection { CRYPTO_TO_FIAT, FIAT_TO_CRYPTO }