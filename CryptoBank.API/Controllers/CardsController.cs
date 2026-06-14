using CryptoBank.Application.Services.CardService;
using CryptoBank.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBank.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class CardsController(ICardService cardService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUserCards()
    {
        var result = await cardService.GetAllUserCardsAsync(User.GetUserId());
        return Ok(result);
    }

    [HttpGet("{cardId:long}")]
    public async Task<IActionResult> GetCardById(long cardId)
    {
        var result = await cardService.GetCardByIdAsync(User.GetUserId(), cardId);
        return  result is null ? NotFound() : Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateCard([FromBody] long currencyId)
    {
        await cardService.CreateCardAsync(User.GetUserId(), currencyId);
        return Ok();
    }
}