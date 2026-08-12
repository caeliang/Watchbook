namespace WatchBook.Domain.Enums.Content;

/// <summary>
/// Defines the type of content (movie or TV series).
/// </summary>
public enum ContentType
{
    /// <summary>
    /// A movie (single release).
    /// </summary>
    Movie = 0,

    /// <summary>
    /// A television series with multiple seasons and episodes.
    /// </summary>
    Series = 1
}
