using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Domain.Entities.Relations;

/// <summary>
/// Represents the many-to-many relationship between content and a production country.
/// This is a junction table with no additional payload.
/// </summary>
public class ContentCountry
{
    /// <summary>
    /// The foreign key referencing the content.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The foreign key referencing the country (ISO 3166-1 code).
    /// </summary>
    public string CountryCode { get; set; } = string.Empty;

    /// <summary>
    /// The navigation property to the related content.
    /// </summary>
    public Content Content { get; set; } = null!;

    /// <summary>
    /// The navigation property to the related country.
    /// </summary>
    public Country Country { get; set; } = null!;
}