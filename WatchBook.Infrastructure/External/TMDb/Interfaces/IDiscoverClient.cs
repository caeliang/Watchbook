using WatchBook.Infrastructure.External.TMDb.Responses.Discover;

namespace WatchBook.Infrastructure.External.TMDb.Interfaces;

/// <summary>
/// Provides access to TMDb discover endpoints.
/// </summary>
public interface IDiscoverClient
{
    Task<DiscoverResponse> DiscoverMoviesAsync(
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<DiscoverResponse> DiscoverTvSeriesAsync(
        int page = 1,
        CancellationToken cancellationToken = default);
}