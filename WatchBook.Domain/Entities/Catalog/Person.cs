using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Relations;

namespace WatchBook.Domain.Entities.Catalog;

/// <summary>
/// Represents a person involved in content production (actor, director, writer, etc.).
/// </summary>
public class Person : BaseEntity
{
    /// <summary>
    /// The unique identifier of the person in TMDb.
    /// </summary>
    public int TmdbId { get; set; }

    /// <summary>
    /// The full name of the person.
    /// </summary>
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// The original name of the person in their native language.
    /// </summary>
    [MaxLength(200)]
    public string? OriginalName { get; set; }

    /// <summary>
    /// A biographical description of the person.
    /// </summary>
    public string? Biography { get; set; }

    /// <summary>
    /// The person's birth date.
    /// </summary>
    public DateOnly? Birthday { get; set; }

    /// <summary>
    /// The person's death date, if applicable.
    /// </summary>
    public DateOnly? Deathday { get; set; }

    /// <summary>
    /// The place where the person was born.
    /// </summary>
    [MaxLength(200)]
    public string? PlaceOfBirth { get; set; }

    /// <summary>
    /// The relative path of the person's profile image provided by TMDb.
    /// </summary>
    [MaxLength(500)]
    public string? ProfilePath { get; set; }

    /// <summary>
    /// Indicates whether this person is an adult performer.
    /// </summary>
    public bool IsAdult { get; set; }

    /// <summary>
    /// The popularity score provided by TMDb.
    /// </summary>
    public double Popularity { get; set; }

    /// <summary>
    /// The content roles associated with this person.
    /// </summary>
    public ICollection<ContentPerson> ContentPeople { get; set; } = [];
}