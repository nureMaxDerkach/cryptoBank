using System.Security.Claims;

namespace CryptoBank.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static long GetUserId(this ClaimsPrincipal user)
    {
        var idClaim = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (idClaim == null)
        {
            throw new InvalidOperationException("User id claim not found");
        }

        return long.Parse(idClaim);
    }
}