using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a season of a TV series.
/// </summary>
public class Season : BaseEntity
{
    /// <summary>
    /// The unique identifier of the season in TMDb.
    /// </summary>
    public int TmdbId { get; set; }

    /// <summary>
    /// The identifier of the content (TV series) to which this season belongs.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The season number. 0 indicates specials/pilots.
    /// </summary>
    [Range(0, 999)]
    public int SeasonNumber { get; set; }

    /// <summary>
    /// The name or title of the season.
    /// </summary>
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A brief description of the season.
    /// </summary>
    public string? Overview { get; set; }

    /// <summary>
    /// The relative path of the season poster image.
    /// </summary>
    [MaxLength(500)]
    public string? PosterPath { get; set; }

    /// <summary>
    /// The air date of the season premiere.
    /// </summary>
    public DateOnly? AirDate { get; set; }

    /// <summary>
    /// The total number of episodes in this season.
    /// </summary>
    [Range(0, 9999)]
    public int EpisodeCount { get; set; }

    /// <summary>
    /// The TV series to which this season belongs.
    /// </summary>
    public Content Content { get; set; } = null!;

    /// <summary>
    /// The episodes that belong to this season.
    /// </summary>
    public ICollection<Episode> Episodes { get; set; } = [];
}