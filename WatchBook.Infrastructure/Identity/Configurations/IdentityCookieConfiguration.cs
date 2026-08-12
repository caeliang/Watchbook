using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace WatchBook.Infrastructure.Identity.Configurations;

/// <summary>
/// Configures the authentication cookie used by ASP.NET Core Identity.
/// </summary>
public static class IdentityCookieConfiguration
{
    /// <summary>
    /// Configures the application authentication cookie.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The service collection.</returns>
    public static IServiceCollection AddIdentityCookieConfiguration(
        this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.ConfigureApplicationCookie(options =>
        {
            // Cookie
            options.Cookie.Name = "WatchBook.Auth";
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

            // Paths
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
            options.AccessDeniedPath = "/Auth/AccessDenied";

            // Expiration
            options.ExpireTimeSpan = TimeSpan.FromDays(30);
            options.SlidingExpiration = true;

            // Authentication
            options.ReturnUrlParameter = "returnUrl";
        });

        return services;
    }
}