using CryptoBank.Application.Abstractions;
using CryptoBank.Contracts.Requests;
using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;

namespace CryptoBank.Application.Services.AuthService;

public class AuthService(
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork, 
    ITokenService tokenService) : IAuthService
{
    public async Task<RegisterResponse> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            DateOfBirth = request.DateOfBirth,
            CountryId = request.CountryId,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };

        await userRepository.AddAsync(user);
        await unitOfWork.SaveChangesAsync();

        var token = tokenService.GenerateAccessToken(user);

        return new RegisterResponse(token);
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
        {
            throw new UnauthorizedAccessException("Invalid credentials");
        }
        
        var token = tokenService.GenerateAccessToken(user);
        return new LoginResponse(token);
    }
}