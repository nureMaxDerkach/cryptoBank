using CryptoBank.Domain.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace CryptoBank.Domain.Models;

public class ExchangeRate : BaseEntity
{
    public long CurrencyId { get; set; }

    public Currency? Currency { get; set; }

    [Precision(38, 18)]
    public decimal RateToUsd { get; set; }

    public DateTime UpdatedAt { get; set; }
}