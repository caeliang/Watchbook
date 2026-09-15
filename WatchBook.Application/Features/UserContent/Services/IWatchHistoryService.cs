namespace WatchBook.Application.Features.UserContent.Services;

public interface IWatchHistoryService
{
    Task AddAsync(
        string userId,
        int contentId,
        int? episodeId,
        decimal? rating,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string userId,
        int historyId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<WatchHistoryItem>> GetAsync(
        string userId,
        CancellationToken cancellationToken = default);
}

public sealed record WatchHistoryItem(
    int Id,
    int ContentId,
    int? EpisodeId,
    string Title,
    int? SeasonNumber,
    int? EpisodeNumber,
    string? EpisodeName,
    DateTime WatchedAt,
    decimal? Rating);