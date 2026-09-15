using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using WatchBook.Infrastructure.Features.System.Interfaces;

namespace WatchBook.Infrastructure.Features.System.Services;

/// <summary>
/// Provides access to the current authenticated user.
/// </summary>
public sealed class CurrentUserService(
    IHttpContextAccessor httpContextAccessor)
    : ICurrentUserService
{
    private ClaimsPrincipal? User =>
        httpContextAccessor.HttpContext?.User;

    public string? UserId =>
        User?.FindFirstValue(ClaimTypes.NameIdentifier);

    public string? UserName =>
        User?.Identity?.Name;

    public string? Email =>
        User?.FindFirstValue(ClaimTypes.Email);

    public bool IsAuthenticated =>
        User?.Identity?.IsAuthenticated ?? false;
}