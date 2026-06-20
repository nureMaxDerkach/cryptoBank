using CryptoBank.Application.Services.AuthService;
using CryptoBank.Application.Services.CardService;
using CryptoBank.Application.Services.ExchangeRateService;
using CryptoBank.Application.Services.TransactionService;
using CryptoBank.Application.Services.WalletService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoBank.Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<ICardService, CardService>();
        services.AddScoped<IWalletService, WalletService>();
        services.AddScoped<IExchangeRateService, ExchangeRateService>();
        services.AddScoped<ITransactionService, TransactionService>();
        
        return services;
    }
}