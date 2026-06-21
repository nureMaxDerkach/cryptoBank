using CryptoBank.Application.Services.UserService;
using CryptoBank.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CryptoBank.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class UsersController(IUserService userService) : ControllerBase
{
    [HttpGet("me")]
    public async Task<IActionResult> GetMyDetails()
    {
        var userId = User.GetUserId();
        var result = await userService.GetMyDetailsAsync(userId);
        return Ok(result);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        var result = await userService.GetAllUsersAsync();
        return Ok(result);
    }
}