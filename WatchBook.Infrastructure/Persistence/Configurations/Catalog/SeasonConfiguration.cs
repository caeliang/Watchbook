using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Season"/> entity.
/// </summary>
public class SeasonConfiguration : BaseEntityConfiguration<Season>
{
    /// <summary>
    /// Configures the Season entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Season.</param>
    public override void Configure(EntityTypeBuilder<Season> builder)
    {
        base.Configure(builder);

        builder.ToTable("Seasons");

        // Configure properties
        builder
            .Property(s => s.TmdbId)
            .IsRequired();

        builder.HasIndex(s => s.TmdbId)
            .IsUnique()
            .HasDatabaseName("IX_Seasons_TmdbId");

        builder
            .Property(s => s.ContentId)
            .IsRequired();

        builder
            .Property(s => s.SeasonNumber)
            .IsRequired();

        builder
            .Property(s => s.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(s => s.Overview)
            .HasColumnType("nvarchar(max)");

        builder
            .Property(s => s.PosterPath)
            .HasMaxLength(500);

        builder
            .Property(s => s.AirDate)
            .HasColumnType("date");

        builder
            .Property(s => s.EpisodeCount)
            .IsRequired();

        // Configure relationships
        builder
            .HasOne(s => s.Content)
            .WithMany(c => c.Seasons)
            .HasForeignKey(s => s.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Seasons_Contents_ContentId");

        builder
            .HasMany(s => s.Episodes)
            .WithOne(e => e.Season)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Episodes_Seasons_SeasonId");

        // Create indexes for common queries
        builder.HasIndex(s => new { s.ContentId, s.SeasonNumber })
            .IsUnique()
            .HasDatabaseName("IX_Seasons_ContentId_SeasonNumber");

        builder.HasIndex(s => s.ContentId)
            .HasDatabaseName("IX_Seasons_ContentId");
    }
}
