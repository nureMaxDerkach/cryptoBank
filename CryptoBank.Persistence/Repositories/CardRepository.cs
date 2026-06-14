using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class CardRepository(CryptoBankDbContext dbContext) : ICardRepository
{
    public async Task<Card> AddCardAsync(Card card)
    {
        var entity = await dbContext.Cards.AddAsync(card);
        return entity.Entity;
    }

    public async Task<List<Card>> GetAllCardsByUserIdAsync(long userId)
    {
        return await dbContext.Cards.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Card?> GetCardByIdAsync(long userId, long cardId)
    {
        return await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == cardId && x.UserId == userId);
    }
}