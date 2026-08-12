namespace WatchBook.Infrastructure.Services.Interfaces;

/// <summary>
/// Builds TMDb image URLs.
/// </summary>
public interface IImageUrlBuilder
{
    string GetPosterUrl(string? path);

    string GetBackdropUrl(string? path);

    string GetProfileUrl(string? path);

    string GetLogoUrl(string? path);

    string GetStillUrl(string? path);
}