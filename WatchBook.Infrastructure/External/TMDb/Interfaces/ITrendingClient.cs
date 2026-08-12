using WatchBook.Domain.Enums.Discovery;
using WatchBook.Infrastructure.External.TMDb.Responses.Trending;

namespace WatchBook.Infrastructure.External.TMDb.Interfaces;

/// <summary>
/// Provides access to TMDb trending endpoints.
/// </summary>
public interface ITrendingClient
{
    Task<TrendingResponse> GetTrendingMoviesAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default);

    Task<TrendingResponse> GetTrendingTvSeriesAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default);

    Task<TrendingResponse> GetTrendingPeopleAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default);

    Task<TrendingResponse> GetTrendingAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default);
}