namespace WatchBook.Domain.Enums.Content;

/// <summary>
/// Defines the watch status of content as tracked by a user.
/// </summary>
public enum ContentWatchStatus
{
    /// <summary>
    /// User has marked the content to watch in the future.
    /// </summary>
    PlanToWatch = 0,

    /// <summary>
    /// User is currently watching this content.
    /// </summary>
    Watching = 1,

    /// <summary>
    /// User has paused watching this content.
    /// </summary>
    OnHold = 2,

    /// <summary>
    /// User has finished watching all content.
    /// </summary>
    Completed = 3,

    /// <summary>
    /// User has stopped watching (did not finish).
    /// </summary>
    Dropped = 4
}
