using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Domain.Enums.Content;

namespace WatchBook.Infrastructure.Persistence.Configurations.Relations;

/// <summary>
/// Fluent API configuration for the <see cref="ContentPerson"/> entity.
/// This is a rich join table with additional metadata about person roles on content.
/// </summary>
public class ContentPersonConfiguration : IEntityTypeConfiguration<ContentPerson>
{
    /// <summary>
    /// Configures the ContentPerson entity with key and relationships.
    /// </summary>
    /// <param name="builder">The entity type builder for ContentPerson.</param>
    public void Configure(EntityTypeBuilder<ContentPerson> builder)
    {
        builder.ToTable("ContentPeople");

        // Configure key
        builder.HasKey(cp => cp.Id);

        // Configure properties
        builder
            .Property(cp => cp.ContentId)
            .IsRequired();

        builder
            .Property(cp => cp.PersonId)
            .IsRequired();

        builder
            .Property(cp => cp.Role)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(cp => cp.CharacterName)
            .HasMaxLength(200);

        builder
            .Property(cp => cp.DisplayOrder)
            .HasDefaultValue(0)
            .IsRequired();

        // Configure relationships
        builder
            .HasOne(cp => cp.Content)
            .WithMany(c => c.ContentPeople)
            .HasForeignKey(cp => cp.ContentId)
            .OnDelete(DeleteBehavior.Cascade)
            .HasConstraintName("FK_ContentPeople_Contents_ContentId");

        builder
            .HasOne(cp => cp.Person)
            .WithMany(p => p.ContentPeople)
            .HasForeignKey(cp => cp.PersonId)
            .OnDelete(DeleteBehavior.Restrict)
            .HasConstraintName("FK_ContentPeople_People_PersonId");

        // Create indexes
        builder.HasIndex(cp => new
        {
            cp.ContentId,
            cp.Role,
            cp.DisplayOrder
        })
            .HasDatabaseName("IX_ContentPeople_ContentId_Role");

        builder.HasIndex(cp => cp.PersonId)
            .HasDatabaseName("IX_ContentPeople_PersonId");


        builder.HasIndex(cp => cp.DisplayOrder)
            .HasDatabaseName("IX_ContentPeople_DisplayOrder");
    }
}
