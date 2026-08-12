using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Infrastructure.Persistence.Configurations.Relations;

/// <summary>
/// Fluent API configuration for the <see cref="ContentCountry"/> entity (many-to-many junction table).
/// </summary>
public class ContentCountryConfiguration : IEntityTypeConfiguration<ContentCountry>
{
    /// <summary>
    /// Configures the ContentCountry entity with composite key and relationships.
    /// </summary>
    /// <param name="builder">The entity type builder for ContentCountry.</param>
    public void Configure(EntityTypeBuilder<ContentCountry> builder)
    {
        builder.ToTable("ContentCountries");

        // Configure composite key
        builder.HasKey(cc => new { cc.ContentId, cc.CountryCode });

        // Configure properties
        builder
            .Property(cc => cc.ContentId)
            .IsRequired();

        builder
            .Property(cc => cc.CountryCode)
            .HasMaxLength(10)
            .IsRequired();

        // Configure relationships
        builder
            .HasOne(cc => cc.Content)
            .WithMany(c => c.ContentCountries)
            .HasForeignKey(cc => cc.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentCountries_Contents_ContentId");

        builder
            .HasOne(cc => cc.Country)
            .WithMany(co => co.ContentCountries)
            .HasForeignKey(cc => cc.CountryCode)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentCountries_Countries_Code");

        // Create indexes
        builder.HasIndex(cc => cc.CountryCode)
.HasDatabaseName("IX_ContentCountries_CountryCode");
    }
}
