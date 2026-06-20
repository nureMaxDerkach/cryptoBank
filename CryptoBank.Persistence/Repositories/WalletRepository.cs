using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class WalletRepository(CryptoBankDbContext dbContext) : IWalletRepository
{
    public async Task<Wallet> AddAsync(Wallet wallet)
    {
        var entity = await dbContext.Wallets.AddAsync(wallet);
        return entity.Entity;
    }

    public async Task<List<Wallet>> GetAllBuUserIdAsync(long userId)
    {
        return await dbContext.Wallets
            .Where(x => x.UserId == userId)
            .Include(x => x.Currency)
            .ToListAsync();
    }

    public async Task<Wallet?> GetByIdAsync(long walletId)
    {
        return await dbContext.Wallets
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(x => x.Id == walletId);
    }

    public async Task<Wallet?> GetByAddressAsync(string address)
    {
        return await dbContext.Wallets
            .Include(x => x.Currency)
            .FirstOrDefaultAsync(x => x.Address == address);
    }
}