using WatchBook.Infrastructure.External.TMDb.Clients.Base;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;

namespace WatchBook.Infrastructure.External.TMDb.Clients;

public sealed class MovieClient(HttpClient httpClient)
    : TmdbClientBase(httpClient), IMovieClient
{
    public Task<MovieDetailsResponse> GetDetailsAsync(
        int movieId,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieDetailsResponse>(
            $"movie/{movieId}",
            cancellationToken);


    public Task<MovieCreditsResponse> GetCreditsAsync(
        int movieId,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieCreditsResponse>(
            $"movie/{movieId}/credits",
            cancellationToken);


    public Task<MovieImagesResponse> GetImagesAsync(
        int movieId,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieImagesResponse>(
            $"movie/{movieId}/images",
            cancellationToken);


    public Task<MovieVideosResponse> GetVideosAsync(
        int movieId,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieVideosResponse>(
            $"movie/{movieId}/videos",
            cancellationToken);


    public Task<MovieReleaseDatesResponse> GetReleaseDatesAsync(
        int movieId,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieReleaseDatesResponse>(
            $"movie/{movieId}/release_dates",
            cancellationToken);


    public Task<MovieWatchProvidersResponse> GetWatchProvidersAsync(
        int movieId,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieWatchProvidersResponse>(
            $"movie/{movieId}/watch/providers",
            cancellationToken);


    public Task<MovieListResponse> GetPopularAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieListResponse>(
            $"movie/popular?page={page}",
            cancellationToken);


    public Task<MovieListResponse> GetTopRatedAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieListResponse>(
            $"movie/top_rated?page={page}",
            cancellationToken);


    public Task<MovieListResponse> GetUpcomingAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieListResponse>(
            $"movie/upcoming?page={page}",
            cancellationToken);


    public Task<MovieListResponse> GetNowPlayingAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<MovieListResponse>(
            $"movie/now_playing?page={page}",
            cancellationToken);
}