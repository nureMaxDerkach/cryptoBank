using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Persistence.Repositories;

public class CardRepository(CryptoBankDbContext dbContext) : ICardRepository
{
    public async Task<Card> AddAsync(Card card)
    {
        var entity = await dbContext.Cards.AddAsync(card);
        return entity.Entity;
    }

    public async Task<List<Card>> GetAllByUserIdAsync(long userId)
    {
        return await dbContext.Cards.Where(x => x.UserId == userId).ToListAsync();
    }

    public async Task<Card?> GetByIdAsync(long cardId)
    {
        return await dbContext.Cards.FirstOrDefaultAsync(x => x.Id == cardId);
    }

    public async Task<Card?> GetByCardNumberAsync(string cardNumber)
    {
        return await dbContext.Cards.FirstOrDefaultAsync(x => x.CardNumber == cardNumber);
    }
}