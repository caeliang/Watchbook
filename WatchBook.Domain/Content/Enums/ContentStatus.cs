namespace WatchBook.Domain.Enums.Content;

/// <summary>
/// Defines the lifecycle status of content within the system.
/// </summary>
public enum ContentStatus
{
    /// <summary>
    /// The content is active and visible to users.
    /// </summary>
    Active = 0,

    /// <summary>
    /// The content is hidden but not deleted (soft-deleted state).
    /// </summary>
    Hidden = 1,

    /// <summary>
    /// The content is marked for deletion (soft-deleted).
    /// </summary>
    Deleted = 2
}
