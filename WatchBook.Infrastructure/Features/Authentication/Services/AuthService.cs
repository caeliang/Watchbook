using Microsoft.AspNetCore.Identity;
using WatchBook.Application.Features.Authentication.Services;
using WatchBook.Infrastructure.Features.Authentication.Identity;

namespace WatchBook.Infrastructure.Features.Authentication.Services;

public sealed class AuthService(
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager) : IAuthService
{
    public async Task RegisterAsync(
        string email,
        string password,
        string? displayName,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        email = email.Trim();

        var existingUser = await userManager.FindByEmailAsync(email);

        if (existingUser is not null)
        {
            throw new InvalidOperationException(
                "A user with this email address already exists.");
        }

        var user = new ApplicationUser
        {
            UserName = email,
            Email = email,
            DisplayName = string.IsNullOrWhiteSpace(displayName)
                ? null
                : displayName.Trim(),
            CreatedAt = DateTime.UtcNow
        };

        var result = await userManager.CreateAsync(user, password);

        if (!result.Succeeded)
        {
            var errors = string.Join(
                ", ",
                result.Errors.Select(error => error.Description));

            throw new InvalidOperationException(
                $"User registration failed: {errors}");
        }
    }

    public async Task LoginAsync(
        string email,
        string password,
        bool rememberMe = false,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        email = email.Trim();

        var user = await userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new UnauthorizedAccessException(
                "Invalid email or password.");
        }

        var result = await signInManager.PasswordSignInAsync(
            user,
            password,
            rememberMe,
            lockoutOnFailure: true);

        if (result.Succeeded)
        {
            return;
        }

        if (result.IsLockedOut)
        {
            throw new UnauthorizedAccessException(
                "The account is temporarily locked.");
        }

        if (result.IsNotAllowed)
        {
            throw new UnauthorizedAccessException(
                "Sign-in is not allowed for this account.");
        }

        throw new UnauthorizedAccessException(
            "Invalid email or password.");
    }

    public async Task LogoutAsync(
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await signInManager.SignOutAsync();
    }
}