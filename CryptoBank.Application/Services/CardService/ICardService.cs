using CryptoBank.Contracts.Responses;

namespace CryptoBank.Application.Services.CardService;

public interface ICardService
{
    Task CreateCardAsync(long userId, long currencyId);
    
    Task<List<CardResponseDto>> GetAllUserCardsAsync(long userId);
    
    Task<CardResponseDto?> GetCardByIdAsync(long userId, long cardId);
}