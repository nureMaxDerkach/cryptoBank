using CryptoBank.Application.Services.ExchangeRateService;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBank.Controllers;

[ApiController]
[Route("api/currencies")]
public class CurrenciesController(IExchangeRateService exchangeRateService) : ControllerBase
{
    [HttpGet("rates")]
    public async Task<IActionResult> GetRates()
    {
        var result = await exchangeRateService.GetAllRatesAsync();
        return Ok(result);
    }
}