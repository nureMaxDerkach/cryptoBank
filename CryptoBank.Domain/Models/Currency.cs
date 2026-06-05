using System.ComponentModel.DataAnnotations;
using CryptoBank.Domain.Enums;
using CryptoBank.Domain.Models.Base;

namespace CryptoBank.Domain.Models;

public class Currency : BaseEntity
{
    [Required]
    [MaxLength(20)]
    public required string Code { get; set; }

    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }

    public CurrencyType Type { get; set; }
}