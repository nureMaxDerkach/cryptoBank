using CryptoBank.Domain.Interfaces;

namespace CryptoBank.Persistence;

public class UnitOfWork(CryptoBankDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }
}