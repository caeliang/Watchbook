namespace WatchBook.Domain.Common;

/// <summary>
/// The abstract base entity providing common properties for all domain entities.
/// Includes audit fields and soft delete support.
/// </summary>
public abstract class BaseEntity
{
    /// <summary>
    /// The unique identifier for this entity.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The UTC date and time when this entity was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The UTC date and time when this entity was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }

    /// <summary>
    /// The UTC date and time when this entity was soft-deleted, or null if active.
    /// </summary>
    public DateTime? DeletedAt { get; set; }
}
