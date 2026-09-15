using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

namespace WatchBook.Infrastructure.External.TMDb.Interfaces;

/// <summary>
/// Provides access to TMDb TV series endpoints.
/// </summary>
public interface ITvSeriesClient
{
    Task<TvSeriesDetailsResponse> GetDetailsAsync(
        int seriesId,
        CancellationToken cancellationToken = default);

    Task<TvSeriesCreditsResponse> GetCreditsAsync(
        int seriesId,
        CancellationToken cancellationToken = default);

    Task<TvSeriesImagesResponse> GetImagesAsync(
        int seriesId,
        CancellationToken cancellationToken = default);

    Task<TvSeriesVideosResponse> GetVideosAsync(
        int seriesId,
        CancellationToken cancellationToken = default);

    Task<TvSeriesWatchProvidersResponse> GetWatchProvidersAsync(
        int seriesId,
        CancellationToken cancellationToken = default);

    Task<TvExternalIdsResponse> GetExternalIdsAsync(
        int seriesId,
        CancellationToken cancellationToken = default);

    Task<TvSeriesListResponse> GetPopularAsync(
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<TvSeriesListResponse> GetTopRatedAsync(
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<TvSeriesListResponse> GetOnTheAirAsync(
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<TvSeriesListResponse> GetAiringTodayAsync(
        int page = 1,
        CancellationToken cancellationToken = default);

    Task<TvSeasonResponse> GetSeasonAsync(
        int seriesId,
        int seasonNumber,
        CancellationToken cancellationToken = default);

    Task<TvEpisodeResponse> GetEpisodeAsync(
        int seriesId,
        int seasonNumber,
        int episodeNumber,
        CancellationToken cancellationToken = default);
    Task<TvSeasonDetailsResponse> GetSeasonDetailsAsync(
    int tvId,
    int seasonNumber,
    CancellationToken cancellationToken = default);
}