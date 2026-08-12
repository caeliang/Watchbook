using Microsoft.AspNetCore.Identity;

namespace WatchBook.Infrastructure.Identity;

/// <summary>
/// Represents a role in the application with metadata.
/// </summary>
public class ApplicationRole : IdentityRole
{
    /// <summary>
    /// Gets or sets a value indicating whether the role is active.
    /// Inactive roles may be used for soft-deleting or disabling privileges.
    /// </summary>
    public bool IsActive { get; set; } = true;
}
