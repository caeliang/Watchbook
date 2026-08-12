using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a genre category for cataloging content.
/// </summary>
public class Genre : BaseEntity
{
    /// <summary>
    /// The unique identifier of the genre in TMDb.
    /// </summary>
    public int TmdbId { get; set; }

    /// <summary>
    /// The name of the genre (e.g., "Action", "Comedy", "Drama").
    /// </summary>
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Indicates whether this genre is active and available for use.
    /// Inactive genres may be retained for historical data but not assigned to new content.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The content items associated with this genre.
    /// </summary>
    public ICollection<ContentGenre> ContentGenres { get; set; } = [];
}