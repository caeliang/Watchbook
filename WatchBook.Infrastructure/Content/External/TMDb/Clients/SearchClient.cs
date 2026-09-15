using System.Net;
using WatchBook.Infrastructure.External.TMDb.Clients.Base;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Responses.Search;

namespace WatchBook.Infrastructure.External.TMDb.Clients;

/// <summary>
/// Provides access to TMDb search endpoints.
/// </summary>
public sealed class SearchClient(HttpClient httpClient)
    : TmdbClientBase(httpClient), ISearchClient
{
    public Task<SearchMovieListResponse> SearchMoviesAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<SearchMovieListResponse>(
            $"search/movie?query={Uri.EscapeDataString(query)}&page={page}",
            cancellationToken);

    public Task<SearchTvSeriesListResponse> SearchTvSeriesAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<SearchTvSeriesListResponse>(
            $"search/tv?query={Uri.EscapeDataString(query)}&page={page}",
            cancellationToken);

    public Task<SearchPersonListResponse> SearchPeopleAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<SearchPersonListResponse>(
            $"search/person?query={Uri.EscapeDataString(query)}&page={page}",
            cancellationToken);

    public Task<MultiSearchResponse> MultiSearchAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<MultiSearchResponse>(
            $"search/multi?query={Uri.EscapeDataString(query)}&page={page}",
            cancellationToken);
}