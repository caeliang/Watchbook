using WatchBook.Infrastructure.External.TMDb.Responses.Movies;

namespace WatchBook.Infrastructure.External.TMDb.Interfaces;

/// <summary>
/// Provides access to TMDb movie endpoints.
/// </summary>
public interface IMovieClient
{
    /// <summary>
    /// Gets detailed information about a movie.
    /// </summary>
    Task<MovieDetailsResponse> GetDetailsAsync(
        int movieId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets cast and crew information of a movie.
    /// </summary>
    Task<MovieCreditsResponse> GetCreditsAsync(
        int movieId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets images related to a movie.
    /// </summary>
    Task<MovieImagesResponse> GetImagesAsync(
        int movieId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets videos and trailers of a movie.
    /// </summary>
    Task<MovieVideosResponse> GetVideosAsync(
        int movieId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets release dates and certifications of a movie.
    /// </summary>
    Task<MovieReleaseDatesResponse> GetReleaseDatesAsync(
        int movieId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets streaming providers for a movie.
    /// </summary>
    Task<MovieWatchProvidersResponse> GetWatchProvidersAsync(
        int movieId,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets currently playing movies.
    /// </summary>
    Task<MovieListResponse> GetNowPlayingAsync(
        int page = 1,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets popular movies.
    /// </summary>
    Task<MovieListResponse> GetPopularAsync(
        int page = 1,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets top rated movies.
    /// </summary>
    Task<MovieListResponse> GetTopRatedAsync(
        int page = 1,
        CancellationToken cancellationToken = default);


    /// <summary>
    /// Gets upcoming movies.
    /// </summary>
    Task<MovieListResponse> GetUpcomingAsync(
        int page = 1,
        CancellationToken cancellationToken = default);
}