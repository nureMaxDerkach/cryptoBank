using CryptoBank.Application.Services.TransactionService;
using CryptoBank.Contracts.Requests;
using CryptoBank.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBank.Controllers;

[ApiController]
[Authorize]
[Route("api/transactions")]
public class TransactionController(ITransactionService transactionService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTransactions()
    {
        var userId = User.GetUserId();
        var result = await transactionService.GetUserTransactionsAsync(userId);
        return Ok(result);
    }
    
    [HttpPost("convert")]
    public async Task<IActionResult> Convert([FromBody] ConvertRequest request)
    {
        var userId = User.GetUserId();
        var result = await transactionService.ConvertAsync(userId, request);
        return Ok(result);
    }
    
    [HttpPost("sendMoney")]
    public async Task<IActionResult> SendMoney([FromBody] SendMoneyRequest request)
    {
        var userId = User.GetUserId();
        var result = await transactionService.SendMoneyAsync(userId, request);
        return Ok(result);
    }
    
    [HttpPost("sendCrypto")]
    public async Task<IActionResult> SendCrypto([FromBody] SendCryptoRequest request)
    {
        var userId = User.GetUserId();
        var result = await transactionService.SendCryptoAsync(userId, request);
        return Ok(result);
    }
}