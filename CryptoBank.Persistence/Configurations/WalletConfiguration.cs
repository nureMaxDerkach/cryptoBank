using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> builder)
    {
        builder.Property(w => w.Address).HasMaxLength(64).IsRequired();
        builder.Property(w => w.Balance).HasColumnType("decimal(38, 18)").IsRequired();

        builder.HasIndex(w => w.Address).IsUnique();

        builder
            .HasOne(w => w.User)
            .WithMany(u => u.Wallets)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(w => w.Currency)
            .WithMany()
            .HasForeignKey(w => w.CurrencyId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}