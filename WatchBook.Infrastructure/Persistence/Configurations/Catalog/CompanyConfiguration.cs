using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Company"/> entity.
/// </summary>
public class CompanyConfiguration : BaseEntityConfiguration<Company>
{
    /// <summary>
    /// Configures the Company entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Company.</param>
    public override void Configure(EntityTypeBuilder<Company> builder)
    {
        base.Configure(builder);

        builder.ToTable("Companies");

        // Configure properties
        builder
            .Property(c => c.TmdbId)
            .IsRequired();

        builder.HasIndex(c => c.TmdbId)
            .IsUnique()
            .HasDatabaseName    ("IX_Companies_TmdbId");

        builder
            .Property(c => c.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(c => c.Name)
            .HasDatabaseName("IX_Companies_Name");

        builder
            .Property(c => c.Homepage)
            .HasMaxLength(500);

        builder
            .Property(c => c.LogoPath)
            .HasMaxLength(500);

        builder
            .Property(c => c.OriginCountry)
            .HasMaxLength(2);

        builder
            .Property(c => c.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        // Configure relationships
        builder
            .HasMany(c => c.ContentCompanies)
            .WithOne(cc => cc.Company)
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentCompanies_Companies_CompanyId");

        // Create indexes for common queries
        builder.HasIndex(c => c.IsActive)
            .HasDatabaseName("IX_Companies_IsActive");

        builder.HasIndex(c => c.OriginCountry)
            .HasDatabaseName("IX_Companies_OriginCountry");
    }
}
