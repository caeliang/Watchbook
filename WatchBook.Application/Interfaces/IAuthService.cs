namespace WatchBook.Application.Interfaces;

public interface IAuthService
{
    Task RegisterAsync(
        string email,
        string password,
        string? displayName,
        CancellationToken cancellationToken = default);

    Task LoginAsync(
        string email,
        string password,
        bool rememberMe = false,
        CancellationToken cancellationToken = default);

    Task LogoutAsync(
        CancellationToken cancellationToken = default);
}