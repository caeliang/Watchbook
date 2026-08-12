using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Genre"/> entity.
/// </summary>
public class GenreConfiguration : BaseEntityConfiguration<Genre>
{
    /// <summary>
    /// Configures the Genre entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Genre.</param>
    public override void Configure(EntityTypeBuilder<Genre> builder)
    {
        base.Configure(builder);

        builder.ToTable("Genres");

        // Configure properties
        builder
            .Property(g => g.TmdbId)
            .IsRequired();

        builder.HasIndex(g => g.TmdbId)
            .IsUnique()
            .HasDatabaseName("IX_Genres_TmdbId");

        builder
            .Property(g => g.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.HasIndex(g => g.Name)
            .IsUnique()
            .HasDatabaseName("IX_Genres_Name");

        builder
            .Property(g => g.IsActive)
            .HasDefaultValue(true)
            .IsRequired();

        // Configure relationships
        builder
            .HasMany(g => g.ContentGenres)
            .WithOne(cg => cg.Genre)
            .HasForeignKey(cg => cg.GenreId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentGenres_Genres_GenreId");

        // Create indexes for common queries
        builder.HasIndex(g => g.IsActive)
            .HasDatabaseName("IX_Genres_IsActive");
    }
}
