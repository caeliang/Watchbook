using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Infrastructure.Persistence.Configurations.Relations;

/// <summary>
/// Fluent API configuration for the <see cref="ContentNetwork"/> entity (many-to-many junction table).
/// </summary>
public class ContentNetworkConfiguration : IEntityTypeConfiguration<ContentNetwork>
{
    /// <summary>
    /// Configures the ContentNetwork entity with composite key and relationships.
    /// </summary>
    /// <param name="builder">The entity type builder for ContentNetwork.</param>
    public void Configure(EntityTypeBuilder<ContentNetwork> builder)
    {
        builder.ToTable("ContentNetworks");

        // Configure composite key
        builder.HasKey(cn => new { cn.ContentId, cn.NetworkId });

        // Configure properties
        builder
            .Property(cn => cn.ContentId)
            .IsRequired();

        builder
            .Property(cn => cn.NetworkId)
            .IsRequired();

        // Configure relationships
        builder
            .HasOne(cn => cn.Content)
            .WithMany(c => c.ContentNetworks)
            .HasForeignKey(cn => cn.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentNetworks_Contents_ContentId");

        builder
            .HasOne(cn => cn.Network)
            .WithMany(n => n.ContentNetworks)
            .HasForeignKey(cn => cn.NetworkId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentNetworks_Networks_NetworkId");

        // Create indexes
        builder.HasIndex(cn => cn.NetworkId)
            .HasDatabaseName("IX_ContentNetworks_NetworkId");
    }
}
