using CryptoBank.Domain.Interfaces;

namespace CryptoBank.Persistence;

public class UnitOfWork(CryptoBankDbContext dbContext) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync()
    {
        return await dbContext.SaveChangesAsync();
    }

    public async Task<IUnitOfWorkTransaction> BeginTransactionAsync()
    {
        var transaction = await dbContext.Database.BeginTransactionAsync();
        return new UnitOfWorkTransaction(transaction);
    }
}