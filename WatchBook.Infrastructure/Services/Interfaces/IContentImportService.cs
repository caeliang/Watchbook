using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

namespace WatchBook.Infrastructure.Services.Interfaces;

/// <summary>
/// Provides services for importing TMDb content into domain entities.
/// </summary>
public interface IContentImportService
{
    /// <summary>
    /// Imports a movie together with its credits.
    /// </summary>
    Task<Content> ImportMovieAsync(
        int tmdbId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Imports a TV series.
    /// </summary>
    Task<Content> ImportTvSeriesAsync(
        TvSeriesDetailsResponse response,
        CancellationToken cancellationToken = default);
}