using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Network"/> entity.
/// </summary>
public class NetworkConfiguration : BaseEntityConfiguration<Network>
{
    /// <summary>
    /// Configures the Network entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Network.</param>
    public override void Configure(EntityTypeBuilder<Network> builder)
    {
        base.Configure(builder);

        builder.ToTable("Networks");

        // Configure properties
        builder
            .Property(n => n.TmdbId)
            .IsRequired();

        builder.HasIndex(n => n.TmdbId)
            .IsUnique()
            .HasDatabaseName("IX_Networks_TmdbId");

        builder
            .Property(n => n.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(n => n.Name)
            .HasDatabaseName("IX_Networks_Name");

        builder
            .Property(n => n.LogoPath)
            .HasMaxLength(500);

        builder
            .Property(n => n.OriginCountry)
            .HasMaxLength(10);

        builder
            .Property(n => n.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        // Configure relationships
        builder
            .HasMany(n => n.ContentNetworks)
            .WithOne(cn => cn.Network)
            .HasForeignKey(cn => cn.NetworkId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentNetworks_Networks_NetworkId");

        // Create indexes for common queries
        builder.HasIndex(n => n.IsActive)
            .HasDatabaseName("IX_Networks_IsActive");

        builder.HasIndex(n => n.OriginCountry)
            .HasDatabaseName("IX_Networks_OriginCountry");
    }
}
