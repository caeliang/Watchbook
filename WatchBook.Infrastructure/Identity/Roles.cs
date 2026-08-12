namespace WatchBook.Infrastructure.Identity;

/// <summary>
/// Common role names used by the application.
/// Keep values stable because they are persisted in the database and used in authorization checks.
/// </summary>
public static class Roles
{
    /// <summary>
    /// Administrator role with full privileges.
    /// </summary>
    public const string Administrator = "Administrator";

    /// <summary>
    /// Moderator role with content moderation privileges.
    /// </summary>
    public const string Moderator = "Moderator";

    /// <summary>
    /// Regular authenticated user.
    /// </summary>
    public const string User = "User";

    /// <summary>
    /// Read-only guest role for unauthenticated or limited access scenarios.
    /// </summary>
    public const string Guest = "Guest";
}
