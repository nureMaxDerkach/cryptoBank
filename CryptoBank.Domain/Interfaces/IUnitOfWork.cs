namespace CryptoBank.Domain.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}