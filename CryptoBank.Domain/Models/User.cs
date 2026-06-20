using System.ComponentModel.DataAnnotations;
using CryptoBank.Domain.Models.Base;

namespace CryptoBank.Domain.Models;

public class User : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public required string FirstName { get; set; }
    
    [Required]
    [MaxLength(100)]
    public required string LastName { get; set; }
    
    [Required]
    [MaxLength(255)]
    public required string Email { get; set; }

    public DateTime DateOfBirth { get; set; }

    public DateTime CreatedAt { get; set; }

    public long CountryId { get; set; }

    public Country? Country { get; set; }

    public string? PasswordHash { get; set; }

    public ICollection<Card> Cards { get; set; } = new List<Card>();

    public ICollection<Wallet> Wallets { get; set; } = new List<Wallet>();
}