namespace WatchBook.Domain.Enums.Moderation;

/// <summary>
/// Defines the types of moderation actions that can be taken against content or users.
/// </summary>
public enum ModerationActionType
{
    /// <summary>
    /// Permanently delete user-generated content.
    /// </summary>
    DeleteComment = 0,

    /// <summary>
    /// Hide user-generated content from view.
    /// </summary>
    HideComment = 1,

    /// <summary>
    /// Issue a formal warning to a user without restrictions.
    /// </summary>
    WarnUser = 2,

    /// <summary>
    /// Temporarily restrict user access to the platform.
    /// </summary>
    SuspendUser = 3,

    /// <summary>
    /// Permanently restrict user access to the platform.
    /// </summary>
    BanUser = 4
}
