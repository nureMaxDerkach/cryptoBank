using Microsoft.AspNetCore.Mvc;

namespace CryptoBank.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpGet("me")]
    public async Task <IActionResult> GetUsers()
    {
        return Ok("Hello World!");
    }
}