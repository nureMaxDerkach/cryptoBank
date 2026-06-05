using System.ComponentModel.DataAnnotations;

namespace CryptoBank.Domain.Models.Base;

public class BaseEntity
{
    [Key]
    public long Id { get; set; }
}