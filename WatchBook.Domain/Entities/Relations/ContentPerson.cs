using System.ComponentModel.DataAnnotations;
using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Enums.Content;

namespace WatchBook.Domain.Entities.Relations;

/// <summary>
/// Represents the relationship between content and a person with role information.
/// Used to track actors, directors, writers, and other crew members on content.
/// This relation entity includes additional metadata about the person's role.
/// </summary>
public class ContentPerson : BaseEntity
{
    /// <summary>
    /// The foreign key referencing the content.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The foreign key referencing the person.
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// The role of the person on this content (actor, director, writer, etc.).
    /// </summary>
    public PersonRole Role { get; set; }

    /// <summary>
    /// The character name portrayed by the person. Only applicable when Role is Actor.
    /// </summary>
    [MaxLength(200)]
    public string? CharacterName { get; set; }

    /// <summary>
    /// The display order for this person within the context of their role.
    /// Used for ordering credits (e.g., principal cast first).
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// The navigation property to the related content.
    /// </summary>
    public Content Content { get; set; } = null!;

    /// <summary>
    /// The navigation property to the related person.
    /// </summary>
    public Person Person { get; set; } = null!;
}