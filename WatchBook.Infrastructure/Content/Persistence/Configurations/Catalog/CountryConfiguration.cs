using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Country"/> entity.
/// Country is a reference entity without an int Id, using ISO country code as primary key.
/// </summary>
public class CountryConfiguration : IEntityTypeConfiguration<Country>
{
    /// <summary>
    /// Configures the Country entity including key and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Country.</param>
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.ToTable("Countries");

        // Configure key
        builder.HasKey(c => c.Code);

        // Configure properties
        builder
            .Property(c => c.Code)
            .HasMaxLength(10)
            .IsRequired();

        builder
            .Property(c => c.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(c => c.Name)
            .IsUnique()
            .HasDatabaseName("IX_Countries_Name");

        // Configure relationships
        builder
            .HasMany(c => c.ContentCountries)
            .WithOne(cc => cc.Country)
            .HasForeignKey(cc => cc.CountryCode)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentCountries_Countries_Code");
    }
}
