using CryptoBank.Domain.Models;

namespace CryptoBank.Domain.Interfaces;

public interface ICardRepository
{
    Task<Card> AddCardAsync(Card card);
    
    Task<List<Card>> GetAllCardsByUserIdAsync(long userId);
    
    Task<Card?> GetCardByIdAsync(long userId, long cardId);
}