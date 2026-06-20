using CryptoBank.Application.Services.WalletService;
using CryptoBank.Contracts.Requests;
using CryptoBank.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBank.Controllers;

[ApiController]
[Authorize]
[Route("api/wallets")]
public class WalletsController(IWalletService walletService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAllUserWallets()
    {
        var userId = User.GetUserId();
        var result = await walletService.GetAllUserWalletsAsync(userId);
        return Ok(result);
    }
    
    [HttpGet("{walletId:long}")]
    public async Task<IActionResult> GetWalletById(long walletId)
    {
        var userId = User.GetUserId();
        var result = await walletService.GetWalletByIdAsync(userId, walletId);
        return Ok(result);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWallet([FromBody] CreateWalletRequest request)
    {
        var userId = User.GetUserId();
        var result = await walletService.CreateWalletAsync(userId, request.CurrencyId);
        return Ok(result);
    }
}