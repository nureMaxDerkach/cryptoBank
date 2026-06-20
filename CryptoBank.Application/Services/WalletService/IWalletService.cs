using CryptoBank.Contracts.Responses;

namespace CryptoBank.Application.Services.WalletService;

public interface IWalletService
{
    Task<WalletResponse> CreateWalletAsync(long userId, long currencyId);

    Task<List<WalletResponse>> GetAllUserWalletsAsync(long userId);

    Task<WalletResponse> GetWalletByIdAsync(long userId, long walletId);
}