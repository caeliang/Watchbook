using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents an episode belonging to a TV series season.
/// </summary>
public class Episode : BaseEntity
{
    /// <summary>
    /// The unique identifier of the episode in TMDb.
    /// </summary>
    public int TmdbId { get; set; }

    /// <summary>
    /// The identifier of the season to which this episode belongs.
    /// </summary>
    public int SeasonId { get; set; }

    /// <summary>
    /// The episode number within its season. Zero indicates special episodes or pilots.
    /// </summary>
    [Range(0, 9999)]
    public int EpisodeNumber { get; set; }

    /// <summary>
    /// The title of the episode.
    /// </summary>
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// A brief summary or description of the episode.
    /// </summary>
    public string? Overview { get; set; }

    /// <summary>
    /// The original air date of the episode.
    /// </summary>
    public DateOnly? AirDate { get; set; }

    /// <summary>
    /// The runtime of the episode in minutes.
    /// </summary>
    public int? Runtime { get; set; }

    /// <summary>
    /// The relative path of the episode still image (thumbnail) provided by TMDb.
    /// </summary>
    [MaxLength(500)]
    public string? StillPath { get; set; }

    /// <summary>
    /// The season to which this episode belongs.
    /// </summary>
    public Season Season { get; set; } = null!;

    /// <summary>
    /// The average vote score provided by TMDb.
    /// </summary>
    public double? VoteAverage { get; set; }

    /// <summary>
    /// The total number of votes for this episode on TMDb.
    /// </summary>
    public int? VoteCount { get; set; }
}