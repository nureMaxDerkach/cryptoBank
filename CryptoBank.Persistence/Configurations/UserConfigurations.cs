using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");
        
        builder.HasOne(x => x.Country).WithMany().HasForeignKey(x => x.CountryId);
    }
}