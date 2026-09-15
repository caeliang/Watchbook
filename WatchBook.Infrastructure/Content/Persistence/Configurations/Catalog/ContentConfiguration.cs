using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Enums.Content;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Content"/> entity.
/// </summary>
public class ContentConfiguration : BaseEntityConfiguration<Content>
{
    /// <summary>
    /// Configures the Content entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Content.</param>
    public override void Configure(EntityTypeBuilder<Content> builder)
    {
        base.Configure(builder);

        builder.ToTable("Contents");

        // Configure properties
        builder
            .Property(c => c.TmdbId)
            .IsRequired();

        builder.HasIndex(c => c.TmdbId)
            .IsUnique()
            .HasDatabaseName("IX_Contents_TmdbId");

        builder
            .Property(c => c.Type)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(c => c.Title)
            .HasMaxLength(300)
            .IsRequired();

        builder
            .Property(c => c.OriginalTitle)
            .HasMaxLength(300);

        builder
            .Property(c => c.Overview)
            .HasColumnType("nvarchar(max)");

        builder
            .Property(c => c.PosterPath)
            .HasMaxLength(500);

        builder
            .Property(c => c.BackdropPath)
            .HasMaxLength(500);

        builder
            .Property(c => c.ReleaseDate)
            .HasColumnType("date");

        builder
            .Property(c => c.Popularity)
            .HasPrecision(10, 2);

        builder
            .Property(c => c.VoteAverage)
            .HasPrecision(5, 2);

        builder
            .Property(c => c.Status)
            .HasConversion<int>()
            .HasDefaultValue(ContentStatus.Active)
            .IsRequired();

        // Configure relationships
        builder
            .HasMany(c => c.Seasons)
            .WithOne(s => s.Content)
            .HasForeignKey(s => s.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_Seasons_Contents_ContentId");

        builder
            .HasMany(c => c.ContentGenres)
            .WithOne(cg => cg.Content)
            .HasForeignKey(cg => cg.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentGenres_Contents_ContentId");

        builder
            .HasMany(c => c.ContentPeople)
            .WithOne(cp => cp.Content)
            .HasForeignKey(cp => cp.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentPeople_Contents_ContentId");

        builder
            .HasMany(c => c.ContentCompanies)
            .WithOne(cc => cc.Content)
            .HasForeignKey(cc => cc.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentCompanies_Contents_ContentId");

        builder
            .HasMany(c => c.ContentCountries)
            .WithOne(cc => cc.Content)
            .HasForeignKey(cc => cc.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentCountries_Contents_ContentId");

        builder
            .HasMany(c => c.ContentNetworks)
            .WithOne(cn => cn.Content)
            .HasForeignKey(cn => cn.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentNetworks_Contents_ContentId");

        // Create indexes for common queries
        builder.HasIndex(c => c.Type)
            .HasDatabaseName("IX_Contents_Type");

        builder.HasIndex(c => c.Status)
            .HasDatabaseName("IX_Contents_Status");

        builder.HasIndex(c => new { c.Status, c.DeletedAt })
            .HasDatabaseName("IX_Contents_Status_DeletedAt");
    }
}
