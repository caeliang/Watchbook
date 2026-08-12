using System.Security.Claims;

namespace WatchBook.Infrastructure.Identity;

/// <summary>
/// Extension methods for <see cref="ClaimsPrincipal"/> to simplify access to common identity claims.
/// </summary>
public static class ClaimsPrincipalExtensions
{
    /// <summary>
    /// Gets the user identifier from the claims principal, or null if not present.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The user id or null.</returns>
    public static string? GetUserId(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.NameIdentifier)?.Value;

    /// <summary>
    /// Gets the email from the claims principal, or null if not present.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The email or null.</returns>
    public static string? GetEmail(this ClaimsPrincipal principal)
        => principal?.FindFirst(ClaimTypes.Email)?.Value;

    /// <summary>
    /// Gets the display name from the claims principal. Falls back to the Name claim when a dedicated display name is not present.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>The display name or null.</returns>
    public static string? GetDisplayName(this ClaimsPrincipal principal)
        => principal?.FindFirst("displayName")?.Value
           ?? principal?.FindFirst(ClaimTypes.Name)?.Value;

    /// <summary>
    /// Determines whether the principal is an administrator.
    /// </summary>
    /// <param name="principal">The claims principal.</param>
    /// <returns>True when the principal is in the Administrator role.</returns>
    public static bool IsAdministrator(this ClaimsPrincipal principal)
        => principal?.IsInRole(Roles.Administrator) ?? false;
}
