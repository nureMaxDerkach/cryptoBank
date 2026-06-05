using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class CurrencyConfigurations : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        builder.HasData(new Currency
        {
            Id = 1,
            Code = "USD",
            Name = "US Dollar",
            Type = CurrencyType.Fiat
        }, new Currency
        {
            Id = 2,
            Code = "EUR",
            Name = "Euro",
            Type = CurrencyType.Fiat
        }, new Currency
        {
            Id = 3,
            Code = "UAH",
            Name = "Ukrainian hryvnia",
            Type = CurrencyType.Fiat
        }, new Currency
        {
            Id = 4,
            Code = "BTC",
            Name = "Bitcoin",
            Type = CurrencyType.Crypto
        }, new Currency
        {
            Id = 5,
            Code = "ETH",
            Name = "Ethereum",
            Type = CurrencyType.Crypto
        }, new Currency
        {
            Id = 6,
            Code = "TRX",
            Name = "Tronix",
            Type = CurrencyType.Crypto
        }, new Currency
        {
            Id = 7,
            Code = "BNB",
            Name = "Build and Build",
            Type = CurrencyType.Crypto
        }, new Currency
        {
            Id = 8,
            Code = "USDT",
            Name = "Tether USD",
            Type = CurrencyType.Crypto
        }, new Currency
        {
            Id = 9,
            Code = "USDC",
            Name = "USD Coin",
            Type = CurrencyType.Crypto
        });
    }
}