using Microsoft.AspNetCore.Identity;

namespace WatchBook.Infrastructure.Identity;

/// <summary>
/// Represents an application user with additional profile information.
/// </summary>
public class ApplicationUser : IdentityUser
{
    /// <summary>
    /// Gets or sets the display name shown in the UI.
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// Gets or sets the path to the user's profile image.
    /// </summary>
    public string? ProfileImagePath { get; set; }

    /// <summary>
    /// Gets or sets the UTC date when the user was created.
    /// Initialized to DateTime.UtcNow by default.
    /// </summary>
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
