using CryptoBank.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoBank.Persistence.Configurations;

public class CountryConfigurations : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasData(new Country
        {
            Id = 1,
            Name = "Ukraine"
        },
        new Country
        {
            Id = 2,
            Name = "Germany"
        },
        new Country
        {
            Id = 3,
            Name = "United States"
        },
        new Country
        {
            Id = 4,
            Name = "United Kingdom"
        },
        new Country
        {
            Id = 5,
            Name = "Canada"
        },
        new Country
        {
            Id = 6,
            Name = "Singapore"
        },
        new Country
        {
            Id = 7,
            Name = "Switzerland"
        },
        new Country
        {
            Id = 8,
            Name = "United Arab Emirates"
        },
        new Country
        {
            Id = 9,
            Name = "Netherlands"
        },
        new Country
        {
            Id = 10,
            Name = "Estonia"
        });
    }
}