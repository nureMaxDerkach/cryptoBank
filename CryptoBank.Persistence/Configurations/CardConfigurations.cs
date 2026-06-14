using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class CardConfigurations : IEntityTypeConfiguration<Card>
{
    public void Configure(EntityTypeBuilder<Card> builder)
    {
        builder.HasIndex(c => c.CardNumber).IsUnique();
        builder.Property(c => c.CardNumber).HasMaxLength(19).IsRequired();
        builder.Property(c => c.Cvv).HasColumnName("CVV").HasMaxLength(3).IsRequired();
        builder.Property(c => c.Balance).HasColumnType("decimal(18, 2)").IsRequired();
        
        builder
            .HasOne(c => c.User)
            .WithMany(u => u.Cards)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(c => c.Currency)
            .WithMany()
            .HasForeignKey(c => c.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}