namespace WatchBook.Domain.Enums.Moderation;

/// <summary>
/// Defines the moderation status of user comments.
/// </summary>
public enum CommentStatus
{
    /// <summary>
    /// The comment is visible to other users.
    /// </summary>
    Visible = 0,

    /// <summary>
    /// The comment is hidden from view (soft-censored).
    /// </summary>
    Hidden = 1,

    /// <summary>
    /// The comment has been deleted (soft-deleted).
    /// </summary>
    Deleted = 2
}
