namespace WatchBook.Domain.Enums.Content;

/// <summary>
/// Defines the role a person plays in content production.
/// </summary>
public enum PersonRole
{
    /// <summary>
    /// The person performed in the content.
    /// </summary>
    Actor = 1,

    /// <summary>
    /// The person directed the content.
    /// </summary>
    Director = 2,

    /// <summary>
    /// The person wrote the content or screenplay.
    /// </summary>
    Writer = 3,

    /// <summary>
    /// The person produced the content.
    /// </summary>
    Producer = 4,

    /// <summary>
    /// The person composed the music or soundtrack.
    /// </summary>
    Composer = 5,

    /// <summary>
    /// The person served as cinematographer (director of photography).
    /// </summary>
    Cinematographer = 6
}
