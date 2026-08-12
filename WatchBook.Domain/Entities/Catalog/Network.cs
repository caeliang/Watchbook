using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a television network that broadcasts TV series.
/// </summary>
public class Network : BaseEntity
{
    /// <summary>
    /// The unique identifier of the network in TMDb.
    /// </summary>
    public int TmdbId { get; set; }

    /// <summary>
    /// The name of the television network.
    /// </summary>
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The relative path to the network's logo image provided by TMDb.
    /// </summary>
    [MaxLength(500)]
    public string? LogoPath { get; set; }

    /// <summary>
    /// The ISO 3166-1 country code where the network operates.
    /// </summary>
    [MaxLength(10)]
    public string? OriginCountry { get; set; }

    /// <summary>
    /// Indicates whether this network is active.
    /// Inactive networks may be retained for historical data but not assigned to new content.
    /// </summary>
    public bool IsActive { get; set; } = true;

    /// <summary>
    /// The TV series broadcast by this network.
    /// </summary>
    public ICollection<ContentNetwork> ContentNetworks { get; set; } = [];
}