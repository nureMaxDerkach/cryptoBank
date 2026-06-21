using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.Property(t => t.FromAmount).HasColumnType("decimal(38, 18)").IsRequired();
        builder.Property(t => t.ToAmount).HasColumnType("decimal(38, 18)").IsRequired();
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

        builder.HasIndex(t => t.UserId);
        builder.HasIndex(t => t.CreatedAt);
        
        builder.HasOne(t => t.User)
            .WithMany()
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.RecipientUser)
            .WithMany()
            .HasForeignKey(t => t.RecipientUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.FromCurrency)
            .WithMany()
            .HasForeignKey(t => t.FromCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.ToCurrency)
            .WithMany()
            .HasForeignKey(t => t.ToCurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}