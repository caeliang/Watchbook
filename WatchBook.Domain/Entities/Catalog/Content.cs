using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;
using WatchBook.Domain.Enums.Content;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a movie or TV series.
/// </summary>
public class Content : BaseEntity
{
    /// <summary>
    /// The unique identifier of the content in TMDb.
    /// </summary>
    public int TmdbId { get; set; }


    /// <summary>
    /// The type of content.
    /// Movie or Series.
    /// </summary>
    public ContentType Type { get; set; }


    /// <summary>
    /// The main title of the content.
    /// </summary>
    [MaxLength(300)]
    public string Title { get; set; } = string.Empty;


    /// <summary>
    /// The original title of the content.
    /// </summary>
    [MaxLength(300)]
    public string? OriginalTitle { get; set; }


    /// <summary>
    /// The description of the content.
    /// </summary>
    public string? Overview { get; set; }


    /// <summary>
    /// The poster image path.
    /// </summary>
    [MaxLength(500)]
    public string? PosterPath { get; set; }


    /// <summary>
    /// The backdrop image path.
    /// </summary>
    [MaxLength(500)]
    public string? BackdropPath { get; set; }


    /// <summary>
    /// The release date of the content.
    /// </summary>
    public DateOnly? ReleaseDate { get; set; }


    /// <summary>
    /// Runtime in minutes.
    /// Used mainly for movies.
    /// </summary>
    public int? Runtime { get; set; }


    /// <summary>
    /// TMDb popularity score.
    /// </summary>
    public double Popularity { get; set; }


    /// <summary>
    /// TMDb average vote score.
    /// </summary>
    public double VoteAverage { get; set; }


    /// <summary>
    /// Number of TMDb votes.
    /// </summary>
    public int VoteCount { get; set; }


    /// <summary>
    /// Current content status.
    /// </summary>
    public ContentStatus Status { get; set; }

    /// <summary>
    /// Production status reported by TMDb.
    /// </summary>
    public ProductionStatus ProductionStatus { get; set; }
    /// <summary>
    /// The seasons of a TV series. Only populated for series content.
    /// </summary>
    public ICollection<Season> Seasons { get; set; } = [];

    /// <summary>
    /// The genres associated with this content.
    /// </summary>
    public ICollection<ContentGenre> ContentGenres { get; set; } = [];

    /// <summary>
    /// The people (actors, directors, writers, etc.) associated with this content.
    /// </summary>
    public ICollection<ContentPerson> ContentPeople { get; set; } = [];

    /// <summary>
    /// The production and distribution companies associated with this content.
    /// </summary>
    public ICollection<ContentCompany> ContentCompanies { get; set; } = [];

    /// <summary>
    /// The production countries associated with this content.
    /// </summary>
    public ICollection<ContentCountry> ContentCountries { get; set; } = [];

    /// <summary>
    /// The television networks that broadcast this content (for TV series only).
    /// </summary>
    public ICollection<ContentNetwork> ContentNetworks { get; set; } = [];
}