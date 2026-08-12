using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Persistence.Configurations.Catalog;

/// <summary>
/// Fluent API configuration for the <see cref="Person"/> entity.
/// </summary>
public class PersonConfiguration : BaseEntityConfiguration<Person>
{
    /// <summary>
    /// Configures the Person entity including relationships and constraints.
    /// </summary>
    /// <param name="builder">The entity type builder for Person.</param>
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.ToTable("People");

        // Configure properties
        builder
            .Property(p => p.TmdbId)
            .IsRequired();

        builder.HasIndex(p => p.TmdbId)
            .IsUnique()
            .HasDatabaseName("IX_People_TmdbId");

        builder
            .Property(p => p.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.HasIndex(p => p.Name)
            .HasDatabaseName("IX_People_Name");

        builder
            .Property(p => p.OriginalName)
            .HasMaxLength(200);

        builder
            .Property(p => p.Biography)
            .HasColumnType("nvarchar(max)");

        builder
            .Property(p => p.Birthday)
            .HasColumnType("date");

        builder
            .Property(p => p.Deathday)
            .HasColumnType("date");

        builder
            .Property(p => p.PlaceOfBirth)
            .HasMaxLength(200);

        builder
            .Property(p => p.ProfilePath)
            .HasMaxLength(500);

        builder
            .Property(p => p.Popularity)
            .HasPrecision(10, 2);

        // Configure relationships
        builder
            .HasMany(p => p.ContentPeople)
            .WithOne(cp => cp.Person)
            .HasForeignKey(cp => cp.PersonId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentPeople_People_PersonId");

        // Create indexes for common queries
        builder.HasIndex(p => p.IsAdult)
            .HasDatabaseName("IX_People_IsAdult");

        builder.HasIndex(p => p.Popularity)
            .HasDatabaseName("IX_People_Popularity");
    }
}
