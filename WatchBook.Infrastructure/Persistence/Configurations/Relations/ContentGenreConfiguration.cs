using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Infrastructure.Persistence.Configurations.Relations;

/// <summary>
/// Fluent API configuration for the <see cref="ContentGenre"/> entity (many-to-many junction table).
/// </summary>
public class ContentGenreConfiguration : IEntityTypeConfiguration<ContentGenre>
{
    /// <summary>
    /// Configures the ContentGenre entity with composite key and relationships.
    /// </summary>
    /// <param name="builder">The entity type builder for ContentGenre.</param>
    public void Configure(EntityTypeBuilder<ContentGenre> builder)
    {
        builder.ToTable("ContentGenres");

        // Configure composite key
        builder.HasKey(cg => new { cg.ContentId, cg.GenreId });

        // Configure properties
        builder
            .Property(cg => cg.ContentId)
            .IsRequired();

        builder
            .Property(cg => cg.GenreId)
            .IsRequired();

        // Configure relationships
        builder
            .HasOne(cg => cg.Content)
            .WithMany(c => c.ContentGenres)
            .HasForeignKey(cg => cg.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentGenres_Contents_ContentId");

        builder
            .HasOne(cg => cg.Genre)
            .WithMany(g => g.ContentGenres)
            .HasForeignKey(cg => cg.GenreId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentGenres_Genres_GenreId");

        // Create indexes
        builder.HasIndex(cg => cg.GenreId)
            .HasDatabaseName("IX_ContentGenres_GenreId");
    }
}
