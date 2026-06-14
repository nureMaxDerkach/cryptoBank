using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;

namespace CryptoBank.Application.Services.CardService;

public class CardService(ICardRepository cardRepository, IUnitOfWork unitOfWork) : ICardService
{
    public async Task CreateCardAsync(long userId, long currencyId)
    {
        var card = new Card
        {
            UserId = userId,
            CurrencyId = currencyId,
            Balance = 0,
            Status = CardStatus.ACTIVE,
            CardNumber = CardNumberGenerator.GenerateCardNumber(),
            Cvv = CardNumberGenerator.GenerateCvv(),
            ExpiryDate = DateOnly.FromDateTime(DateTime.UtcNow.AddYears(3)),
            CreatedAt = DateTime.UtcNow
        };

        await cardRepository.AddCardAsync(card);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task<List<CardResponseDto>> GetAllUserCardsAsync(long userId)
    {
        var cards = await cardRepository.GetAllCardsByUserIdAsync(userId);
        
        return cards
            .Select(x => new CardResponseDto(x.Id, x.CurrencyId, x.CardNumber, x.Cvv, x.Balance, x.ExpiryDate))
            .ToList();
    }

    public async Task<CardResponseDto?> GetCardByIdAsync(long userId, long cardId)
    {
        var card = await cardRepository.GetCardByIdAsync(userId, cardId);

        if (card is null)
        {
            return null;
        }

        return new CardResponseDto(card.Id, card.CurrencyId, card.CardNumber, card.Cvv, card.Balance, card.ExpiryDate);
    }
}