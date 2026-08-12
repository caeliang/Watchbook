using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WatchBook.Domain.Common;

namespace WatchBook.Infrastructure.Persistence.Configurations;

/// <summary>
/// Base class for entity type configurations that inherit from BaseEntity.
/// Provides common configuration patterns for audit fields and soft delete support.
/// </summary>
/// <typeparam name="T">The entity type inheriting from BaseEntity.</typeparam>
public abstract class BaseEntityConfiguration<T> : IEntityTypeConfiguration<T>
    where T : BaseEntity
{
    /// <summary>
    /// Configures the model for a BaseEntity-derived type.
    /// Automatically configures Id as primary key and audit fields.
    /// Subclasses should call base.Configure and then add entity-specific configuration.
    /// </summary>
    /// <param name="builder">The entity type builder.</param>
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        // Configure primary key
        builder.HasKey(e => e.Id);

        // Configure audit fields
        builder
            .Property(e => e.CreatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder
            .Property(e => e.UpdatedAt)
            .HasColumnType("datetime2")
            .IsRequired();

        builder
            .Property(e => e.DeletedAt)
            .HasColumnType("datetime2")
            .IsRequired(false);

        // Create index for soft delete queries (for filtering out deleted entities)
        builder.HasIndex(e => e.DeletedAt)
               .HasDatabaseName($"IX_{typeof(T).Name}_DeletedAt");
    }
}
