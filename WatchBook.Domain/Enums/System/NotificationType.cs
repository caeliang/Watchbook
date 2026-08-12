namespace WatchBook.Domain.Enums.System;

/// <summary>
/// Defines the types of notifications that can be sent to users.
/// </summary>
public enum NotificationType
{
    /// <summary>
    /// A new season of followed TV series has been released.
    /// </summary>
    NewSeason = 0,

    /// <summary>
    /// A new episode of followed TV series has been released.
    /// </summary>
    NewEpisode = 1,

    /// <summary>
    /// A followed movie or series has been released or become available.
    /// </summary>
    ContentReleased = 2,

    /// <summary>
    /// Another user has replied to a comment by the recipient.
    /// </summary>
    CommentReply = 3,

    /// <summary>
    /// Another user has liked a comment by the recipient.
    /// </summary>
    CommentLiked = 4,

    /// <summary>
    /// Another user has started following the recipient.
    /// </summary>
    FollowedUser = 5,

    /// <summary>
    /// A system-generated notification (maintenance, updates, announcements).
    /// </summary>
    System = 6
}
