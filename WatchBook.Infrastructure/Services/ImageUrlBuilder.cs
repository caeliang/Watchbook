using Microsoft.Extensions.Options;
using WatchBook.Infrastructure.External.TMDb.Options;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Infrastructure.Services;

/// <summary>
/// Builds full TMDb image URLs.
/// </summary>
public sealed class ImageUrlBuilder(
    IOptions<TmdbOptions> options)
    : IImageUrlBuilder
{
    private readonly string _baseUrl = options.Value.ImageBaseUrl;

    public string GetPosterUrl(string? path)
        => Build(path, "w500");

    public string GetBackdropUrl(string? path)
        => Build(path, "w1280");

    public string GetProfileUrl(string? path)
        => Build(path, "w500");

    public string GetLogoUrl(string? path)
        => Build(path, "w500");

    public string GetStillUrl(string? path)
        => Build(path, "w500");

    private string Build(string? path, string size)
    {
        if (string.IsNullOrWhiteSpace(path))
            return string.Empty;

        return $"{_baseUrl}{size}{path}";
    }
}