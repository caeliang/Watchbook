using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Infrastructure.Persistence.Configurations.Relations;

/// <summary>
/// Fluent API configuration for the <see cref="ContentCompany"/> entity (many-to-many junction table).
/// </summary>
public class ContentCompanyConfiguration : IEntityTypeConfiguration<ContentCompany>
{
    /// <summary>
    /// Configures the ContentCompany entity with composite key and relationships.
    /// </summary>
    /// <param name="builder">The entity type builder for ContentCompany.</param>
    public void Configure(EntityTypeBuilder<ContentCompany> builder)
    {
        builder.ToTable("ContentCompanies");

        // Configure composite key
        builder.HasKey(cc => new { cc.ContentId, cc.CompanyId });

        // Configure properties
        builder
            .Property(cc => cc.ContentId)
            .IsRequired();

        builder
            .Property(cc => cc.CompanyId)
            .IsRequired();

        // Configure relationships
        builder
            .HasOne(cc => cc.Content)
            .WithMany(c => c.ContentCompanies)
            .HasForeignKey(cc => cc.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentCompanies_Contents_ContentId");

        builder
            .HasOne(cc => cc.Company)
            .WithMany(co => co.ContentCompanies)
            .HasForeignKey(cc => cc.CompanyId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentCompanies_Companies_CompanyId");

        // Create indexes
        builder.HasIndex(cc => cc.CompanyId)
            .HasDatabaseName("IX_ContentCompanies_CompanyId");
    }
}
