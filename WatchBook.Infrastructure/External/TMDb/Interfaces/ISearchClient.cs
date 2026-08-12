using WatchBook.Infrastructure.External.TMDb.Responses.Search;

namespace WatchBook.Infrastructure.External.TMDb.Interfaces;

/// <summary>
/// Provides access to TMDb search endpoints.
/// </summary>
public interface ISearchClient
{
    Task<SearchMovieListResponse> SearchMoviesAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<SearchTvSeriesListResponse> SearchTvSeriesAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<SearchPersonListResponse> SearchPeopleAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<MultiSearchResponse> MultiSearchAsync(
        string query,
        int page = 1,
        CancellationToken cancellationToken = default);
}