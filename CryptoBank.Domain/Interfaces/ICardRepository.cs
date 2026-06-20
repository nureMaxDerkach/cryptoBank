using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface ICardRepository
{
    Task<Card> AddAsync(Card card);
    
    Task<List<Card>> GetAllByUserIdAsync(long userId);
    
    Task<Card?> GetByIdAsync(long cardId);
    
    Task<Card?> GetByCardNumberAsync(string cardNumber);
}