using CryptoBank.Contracts.Requests;
using CryptoBank.Contracts.Responses;

namespace CryptoBank.Application.Services.TransactionService;

public interface ITransactionService
{
    Task<TransactionResponse> ConvertAsync(long userId, ConvertRequest request);

    Task<TransactionResponse> SendMoneyAsync(long userId, SendMoneyRequest request);

    Task<TransactionResponse> SendCryptoAsync(long userId, SendCryptoRequest request);

    Task<List<TransactionResponse>> GetUserTransactionsAsync(long userId);
}