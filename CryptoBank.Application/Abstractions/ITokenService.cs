using CryptoBank.Domain.Models;

namespace CryptoBank.Application.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(User user);
}