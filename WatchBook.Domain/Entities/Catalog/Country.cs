using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a production country associated with content.
/// Uses ISO 3166-1 country codes as the primary key.
/// </summary>
public class Country
{
    /// <summary>
    /// The ISO 3166-1 country code (e.g., "US", "GB", "JP").
    /// This is the primary key for the Country entity.
    /// </summary>
    [MaxLength(10)]
    [Key]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// The English name of the country.
    /// </summary>
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The content items that have this country as a production country.
    /// </summary>
    public ICollection<ContentCountry> ContentCountries { get; set; } = [];
}