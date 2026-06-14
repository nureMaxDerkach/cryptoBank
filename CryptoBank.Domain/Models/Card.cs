using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Models.Base;

namespace CryptoBank.Domain.Models;

public class Card : BaseEntity
{
    public long UserId { get; set; }
    
    public User? User { get; set; }

    public long CurrencyId { get; set; }

    public Currency? Currency { get; set; }

    public required string CardNumber { get; set; }

    public required string Cvv { get; set; }

    public decimal Balance { get; set; }

    public DateOnly ExpiryDate { get; set; }

    public CardStatus Status { get; set; }

    public DateTime CreatedAt { get; set; }
}