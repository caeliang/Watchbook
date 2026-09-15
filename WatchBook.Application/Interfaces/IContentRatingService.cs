namespace WatchBook.Application.Interfaces;

public interface IContentRatingService
{
    Task<ContentRatingSummary?> GetAsync(
        int contentId,
        CancellationToken cancellationToken = default);
}

public sealed record ContentRatingSummary(
    int ContentId,
    decimal? Rating,
    IReadOnlyList<SeasonRatingSummary> Seasons);

public sealed record SeasonRatingSummary(
    int SeasonId,
    int SeasonNumber,
    decimal? Rating,
    IReadOnlyList<EpisodeRatingSummary> Episodes);

public sealed record EpisodeRatingSummary(
    int EpisodeId,
    int EpisodeNumber,
    string EpisodeName,
    decimal Rating);