using CryptoBank.Domain.Models.Base;

namespace CryptoBank.Domain.Models;

public class Wallet : BaseEntity
{
    public long UserId { get; set; }

    public User? User { get; set; }

    public long CurrencyId { get; set; }

    public Currency? Currency { get; set; }

    public required string Address { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }
}