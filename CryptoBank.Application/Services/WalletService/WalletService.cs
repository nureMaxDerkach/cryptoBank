using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;

namespace CryptoBank.Application.Services.WalletService;

public class WalletService(
    IWalletRepository walletRepository, 
    ICurrencyRepository currencyRepository,
    IUnitOfWork unitOfWork) : IWalletService
{
    public async Task<WalletResponse> CreateWalletAsync(long userId, long currencyId)
    {
        var currency = await currencyRepository.GetByIdAsync(currencyId);

        if (currency is null)
        {
            throw new Exception($"Currency with id {currencyId} not found");
        }

        if (currency.Type != CurrencyType.CRYPTO)
        {
            throw new Exception("Wallets can only be created for crypto currencies");
        }

        var wallet = new Wallet
        {
            UserId = userId,
            CurrencyId = currency.Id,
            Address = WalletAddressGenerator.GenerateAddress(),
            Balance = 0,
            CreatedAt = DateTime.UtcNow
        };

        await walletRepository.AddAsync(wallet);
        await unitOfWork.SaveChangesAsync();

        return MapToResponse(wallet, currency.Code);
    }

    public async Task<List<WalletResponse>> GetAllUserWalletsAsync(long userId)
    {
        var wallets = await walletRepository.GetAllBuUserIdAsync(userId);

        return wallets.Select(w => MapToResponse(w, w.Currency!.Code)).ToList();
    }

    public async Task<WalletResponse> GetWalletByIdAsync(long userId, long walletId)
    {
        var wallet = await walletRepository.GetByIdAsync(walletId);

        if (wallet is null)
        {
            throw new Exception($"Wallet with id {walletId} not found");
        }

        if (wallet.UserId != userId)
        {
            throw new UnauthorizedAccessException("You do not have access to this wallet");
        }

        return MapToResponse(wallet, wallet.Currency!.Code);
    }

    private static WalletResponse MapToResponse(Wallet wallet, string currencyCode)
    {
        return new WalletResponse(wallet.Id, wallet.Address, currencyCode, wallet.Balance, wallet.CreatedAt);
    }
}