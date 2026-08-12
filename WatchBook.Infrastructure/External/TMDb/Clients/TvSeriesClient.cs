using WatchBook.Infrastructure.External.TMDb.Clients.Base;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

namespace WatchBook.Infrastructure.External.TMDb.Clients;

public sealed class TvSeriesClient(HttpClient httpClient)
    : TmdbClientBase(httpClient), ITvSeriesClient
{
    public Task<TvSeriesDetailsResponse> GetDetailsAsync(
        int seriesId,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesDetailsResponse>(
            $"tv/{seriesId}",
            cancellationToken);

    public Task<TvSeriesCreditsResponse> GetCreditsAsync(
        int seriesId,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesCreditsResponse>(
            $"tv/{seriesId}/credits",
            cancellationToken);

    public Task<TvSeriesImagesResponse> GetImagesAsync(
        int seriesId,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesImagesResponse>(
            $"tv/{seriesId}/images",
            cancellationToken);

    public Task<TvSeriesVideosResponse> GetVideosAsync(
        int seriesId,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesVideosResponse>(
            $"tv/{seriesId}/videos",
            cancellationToken);

    public Task<TvSeriesWatchProvidersResponse> GetWatchProvidersAsync(
        int seriesId,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesWatchProvidersResponse>(
            $"tv/{seriesId}/watch/providers",
            cancellationToken);

    public Task<TvExternalIdsResponse> GetExternalIdsAsync(
        int seriesId,
        CancellationToken cancellationToken = default)
        => GetAsync<TvExternalIdsResponse>(
            $"tv/{seriesId}/external_ids",
            cancellationToken);

    public Task<TvSeriesListResponse> GetPopularAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesListResponse>(
            $"tv/popular?page={page}",
            cancellationToken);

    public Task<TvSeriesListResponse> GetTopRatedAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesListResponse>(
            $"tv/top_rated?page={page}",
            cancellationToken);

    public Task<TvSeriesListResponse> GetOnTheAirAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesListResponse>(
            $"tv/on_the_air?page={page}",
            cancellationToken);

    public Task<TvSeriesListResponse> GetAiringTodayAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeriesListResponse>(
            $"tv/airing_today?page={page}",
            cancellationToken);

    public Task<TvSeasonResponse> GetSeasonAsync(
        int seriesId,
        int seasonNumber,
        CancellationToken cancellationToken = default)
        => GetAsync<TvSeasonResponse>(
            $"tv/{seriesId}/season/{seasonNumber}",
            cancellationToken);

    public Task<TvEpisodeResponse> GetEpisodeAsync(
        int seriesId,
        int seasonNumber,
        int episodeNumber,
        CancellationToken cancellationToken = default)
        => GetAsync<TvEpisodeResponse>(
            $"tv/{seriesId}/season/{seasonNumber}/episode/{episodeNumber}",
            cancellationToken);
}