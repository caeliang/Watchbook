using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a production or distribution company involved in content creation.
/// </summary>
public class Company : BaseEntity
{
    /// <summary>
    /// The unique identifier of the company in TMDb.
    /// </summary>
    public int TmdbId { get; set; }

    /// <summary>
    /// The official name of the company.
    /// </summary>
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The company's official homepage URL.
    /// </summary>
    [MaxLength(500)]
    public string? Homepage { get; set; }

    /// <summary>
    /// The relative path to the company's logo image provided by TMDb.
    /// </summary>
    [MaxLength(500)]
    public string? LogoPath { get; set; }

    /// <summary>
    /// The ISO 3166-1 country code where the company is based.
    /// </summary>
    [MaxLength(2)]
    public string? OriginCountry { get; set; }

    /// <summary>
    /// Indicates whether this company is active and available for use.
    /// Inactive companies may be retained for historical data but not assigned to new content.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The content items produced or distributed by this company.
    /// </summary>
    public ICollection<ContentCompany> ContentCompanies { get; set; } = [];
}