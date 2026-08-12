using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Domain.Entities.Relations;

/// <summary>
/// Represents the many-to-many relationship between content and a television network.
/// This is a junction table with no additional payload. Typically used for TV series.
/// </summary>
public class ContentNetwork
{
    /// <summary>
    /// The foreign key referencing the content.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The foreign key referencing the network.
    /// </summary>
    public int NetworkId { get; set; }

    /// <summary>
    /// The navigation property to the related content.
    /// </summary>
    public Content Content { get; set; } = null!;

    /// <summary>
    /// The navigation property to the related network.
    /// </summary>
    public Network Network { get; set; } = null!;
}