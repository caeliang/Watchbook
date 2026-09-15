namespace WatchBook.Application.Interfaces;

public interface IWatchHistoryService
{
    Task AddAsync(
        string userId,
        int contentId,
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
    string Title,
    DateTime WatchedAt);