using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class ExchangeRateConfigurations : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        builder.HasData(
            new ExchangeRate
            {
                Id = 1,
                CurrencyId = 1,
                RateToUsd = 1m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 2,
                CurrencyId = 2, 
                RateToUsd = 1.16m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 3,
                CurrencyId = 3,
                RateToUsd = 0.023m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 4,
                CurrencyId = 4,
                RateToUsd = 62229m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 5,
                CurrencyId = 5, 
                RateToUsd = 1670m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 6,
                CurrencyId = 6, 
                RateToUsd = 0.33m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 7,
                CurrencyId = 7, 
                RateToUsd = 592.20m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 8,
                CurrencyId = 8, 
                RateToUsd = 1m,
                UpdatedAt = new DateTime(2026, 6, 5)
            },
            new ExchangeRate
            {
                Id = 9,
                CurrencyId = 9, 
                RateToUsd = 1m,
                UpdatedAt = new DateTime(2026, 6, 5)
            }
        );
    }
}