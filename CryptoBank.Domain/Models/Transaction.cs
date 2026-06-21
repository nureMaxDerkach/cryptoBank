using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Models.Base;

namespace CryptoBank.Domain.Models;

public class Transaction : BaseEntity
{
    public long UserId { get; set; }

    public User? User { get; set; }
    
    public TransactionType Type { get; set; }

    public TransactionStatus Status { get; set; }
    
    public long FromCurrencyId { get; set; }

    public Currency? FromCurrency { get; set; }

    public decimal FromAmount { get; set; }
    
    public long ToCurrencyId { get; set; }

    public Currency? ToCurrency { get; set; }

    public decimal ToAmount { get; set; }
    
    public long? RecipientUserId { get; set; }

    public User? RecipientUser { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsExternal { get; set; }

    public string? RecipientReference { get; set; }
}