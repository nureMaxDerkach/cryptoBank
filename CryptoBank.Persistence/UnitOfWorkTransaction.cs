using CryptoBank.Domain.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;

namespace CryptoBank.Persistence;

public class UnitOfWorkTransaction(IDbContextTransaction transaction) : IUnitOfWorkTransaction
{
    public async Task CommitAsync()
    {
        await transaction.CommitAsync();
    }

    public async Task RollbackAsync()
    {
        await transaction.RollbackAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await transaction.DisposeAsync();
    }
}