using WatchBook.Domain.Enums.Discovery;
using WatchBook.Infrastructure.External.TMDb.Clients.Base;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Responses.Trending;

namespace WatchBook.Infrastructure.External.TMDb.Clients;

public sealed class TrendingClient(HttpClient httpClient)
    : TmdbClientBase(httpClient), ITrendingClient
{
    public Task<TrendingResponse> GetTrendingMoviesAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default)
        => GetAsync<TrendingResponse>(
            $"trending/movie/{window.ToString().ToLowerInvariant()}",
            cancellationToken);

    public Task<TrendingResponse> GetTrendingTvSeriesAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default)
        => GetAsync<TrendingResponse>(
            $"trending/tv/{window.ToString().ToLowerInvariant()}",
            cancellationToken);

    public Task<TrendingResponse> GetTrendingPeopleAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default)
        => GetAsync<TrendingResponse>(
            $"trending/person/{window.ToString().ToLowerInvariant()}",
            cancellationToken);

    public Task<TrendingResponse> GetTrendingAsync(
        TrendingTimeWindow window = TrendingTimeWindow.Day,
        CancellationToken cancellationToken = default)
        => GetAsync<TrendingResponse>(
            $"trending/all/{window.ToString().ToLowerInvariant()}",
            cancellationToken);
}