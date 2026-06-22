using CryptoBank.Application.Abstractions;
using CryptoBank.Application.Services.CardService;
using CryptoBank.Application.Services.WalletService;
using CryptoBank.Contracts.Requests;
using CryptoBank.Contracts.Responses;
using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Interfaces;
using CryptoBank.Domain.Models;

namespace CryptoBank.Application.Services.AuthService;

public class AuthService(
    IUserRepository userRepository, 
    IUnitOfWork unitOfWork, 
    ICardService cardService,
    ITokenService tokenService,
    IWalletService walletService) : IAuthService
{
    public async Task<AuthResponseDto> RegisterAsync(RegisterRequest request)
    {
        await using var transaction = await unitOfWork.BeginTransactionAsync();

        try
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
            
            await cardService.CreateCardAsync(user.Id, (long)GetCurrencyCodeBasedOnCountryId(request.CountryId));
            
            var cryptoCurrencies = new[]
            {
                CurrencyCode.BTC,
                CurrencyCode.ETH,
                CurrencyCode.TRX,
                CurrencyCode.BNB,
                CurrencyCode.USDT,
                CurrencyCode.USDC,
            };

            foreach (var currency in cryptoCurrencies)
            {
                await walletService.CreateWalletAsync(user.Id, (long)currency);
            }
            
            await transaction.CommitAsync();

            var token = tokenService.GenerateAccessToken(user);

            return CreateAuthResponseDto(token, user);
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<AuthResponseDto> LoginAsync(LoginRequest request)
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
        
        return CreateAuthResponseDto(token, user);
    }
    
    private static AuthResponseDto CreateAuthResponseDto(string token, User user)
    {
        return new AuthResponseDto(token, new UserDto(user.Id, user.FirstName, user.LastName, user.Email));
    }

    private static CurrencyCode GetCurrencyCodeBasedOnCountryId(long countryId) =>
        countryId switch
        {
            (long)CountryCode.UKRAINE => CurrencyCode.UAH,
            (long)CountryCode.GERMANY or (long)CountryCode.NETHERLANDS or (long)CountryCode.ESTONIA => CurrencyCode.EUR,
            _ => CurrencyCode.USD
        };
}