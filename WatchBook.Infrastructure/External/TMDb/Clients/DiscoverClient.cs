using WatchBook.Infrastructure.External.TMDb.Clients.Base;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Responses.Discover;

namespace WatchBook.Infrastructure.External.TMDb.Clients;

public sealed class DiscoverClient(HttpClient httpClient)
    : TmdbClientBase(httpClient), IDiscoverClient
{
    public Task<DiscoverResponse> DiscoverMoviesAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<DiscoverResponse>(
            $"discover/movie?page={page}",
            cancellationToken);

    public Task<DiscoverResponse> DiscoverTvSeriesAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<DiscoverResponse>(
            $"discover/tv?page={page}",
            cancellationToken);
}