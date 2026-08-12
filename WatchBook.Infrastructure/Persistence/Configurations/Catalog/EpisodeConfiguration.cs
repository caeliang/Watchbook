using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Episode"/> entity.
/// </summary>
public class EpisodeConfiguration : BaseEntityConfiguration<Episode>
{
    /// <summary>
    /// Configures the Episode entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Episode.</param>
    public override void Configure(EntityTypeBuilder<Episode> builder)
    {
        base.Configure(builder);

        builder.ToTable("Episodes");

        // Configure properties
        builder
            .Property(e => e.TmdbId)
            .IsRequired();

        builder.HasIndex(e => e.TmdbId)
            .IsUnique()
            .HasDatabaseName("IX_Episodes_TmdbId");

        builder
            .Property(e => e.SeasonId)
            .IsRequired();

        builder
            .Property(e => e.EpisodeNumber)
            .IsRequired();

        builder
            .Property(e => e.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(e => e.Overview)
            .HasColumnType("nvarchar(max)");

        builder
            .Property(e => e.AirDate)
            .HasColumnType("date");

        builder
            .Property(e => e.StillPath)
            .HasMaxLength(500);

        builder
            .Property(e => e.VoteAverage)
            .HasPrecision(5, 2);

        // Configure relationships
        builder
            .HasOne(e => e.Season)
            .WithMany(s => s.Episodes)
            .HasForeignKey(e => e.SeasonId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Episodes_Seasons_SeasonId");

        // Create indexes for common queries
        builder.HasIndex(e => new { e.SeasonId, e.EpisodeNumber })
            .IsUnique()
            .HasDatabaseName("IX_Episodes_SeasonId_EpisodeNumber");

        builder.HasIndex(e => e.SeasonId)
            .HasDatabaseName("IX_Episodes_SeasonId");

        builder.HasIndex(e => e.AirDate)
            .HasDatabaseName("IX_Episodes_AirDate");
    }
}
