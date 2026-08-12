using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Domain.Entities.Relations;

/// <summary>
/// Represents the many-to-many relationship between content and a genre.
/// This is a junction table with no additional payload.
/// </summary>
public class ContentGenre
{
    /// <summary>
    /// The foreign key referencing the content.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The foreign key referencing the genre.
    /// </summary>
    public int GenreId { get; set; }

    /// <summary>
    /// The navigation property to the related content.
    /// </summary>
    public Content Content { get; set; } = null!;

    /// <summary>
    /// The navigation property to the related genre.
    /// </summary>
    public Genre Genre { get; set; } = null!;
}