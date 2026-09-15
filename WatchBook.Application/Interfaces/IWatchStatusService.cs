using WatchBook.Domain.Features.UserContent.Enums;

namespace WatchBook.Application.Interfaces;

public interface IWatchStatusService
{
    Task SetAsync(
        string userId,
        int contentId,
        WatchStatusType status,
        CancellationToken cancellationToken = default);

    Task<WatchStatusType?> GetAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default);

    Task RemoveAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default);
}