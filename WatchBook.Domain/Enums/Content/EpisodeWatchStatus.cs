namespace WatchBook.Domain.Enums.Content;

/// <summary>
/// Defines the watch status of individual episodes as tracked by a user.
/// </summary>
public enum EpisodeWatchStatus
{
    /// <summary>
    /// User has not started watching the episode.
    /// </summary>
    NotStarted = 0,

    /// <summary>
    /// User is currently watching the episode.
    /// </summary>
    Watching = 1,

    /// <summary>
    /// User has finished watching the episode.
    /// </summary>
    Completed = 2
}
