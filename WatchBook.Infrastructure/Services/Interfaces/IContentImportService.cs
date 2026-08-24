using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Infrastructure.Services.Interfaces;

/// <summary>
/// Provides services for importing TMDb content into domain entities.
/// </summary>
public interface IContentImportService
{
    /// <summary>
    /// Imports a movie using its TMDb identifier.
    /// </summary>
    Task<Content> ImportMovieAsync(
        int tmdbId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Imports a TV series using its TMDb identifier.
    /// </summary>
    Task<Content> ImportTvSeriesAsync(
        int tmdbId,
        CancellationToken cancellationToken = default);
}