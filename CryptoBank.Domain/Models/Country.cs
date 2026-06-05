using System.ComponentModel.DataAnnotations;
using CryptoBank.Domain.Models.Base;

namespace CryptoBank.Domain.Models;

public class Country : BaseEntity
{
    [Required]
    [MaxLength(50)]
    public required string Name { get; set; }
}