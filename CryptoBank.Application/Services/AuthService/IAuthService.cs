using CryptoBank.Contracts.Requests;
using CryptoBank.Contracts.Responses;
using RegisterRequest = CryptoBank.Contracts.Requests.RegisterRequest;

namespace CryptoBank.Application.Services.AuthService;

public interface IAuthService
{
    Task<AuthResponseDto> RegisterAsync(RegisterRequest request);
    
    Task<AuthResponseDto> LoginAsync(LoginRequest request);
}