using CryptoBank.Contracts.Requests;
using CryptoBank.Contracts.Responses;
using RegisterRequest = CryptoBank.Contracts.Requests.RegisterRequest;

namespace CryptoBank.Application.Services.AuthService;

public interface IAuthService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    
    Task<LoginResponse> LoginAsync(LoginRequest request);
}