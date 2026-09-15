namespace WatchBook.Application.Interfaces;

public interface IWatchlistService
{
    Task AddAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default);
}