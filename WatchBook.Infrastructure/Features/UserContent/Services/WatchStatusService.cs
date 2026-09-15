using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Interfaces;
using WatchBook.Domain.Features.UserContent.Entities;
using WatchBook.Domain.Features.UserContent.Enums;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Features.UserContent.Services;

public sealed class WatchStatusService(
    WatchBookDbContext dbContext) : IWatchStatusService
{
    public async Task SetAsync(
        string userId,
        int contentId,
        WatchStatusType status,
        CancellationToken cancellationToken = default)
    {
        var contentExists = await dbContext.Contents
            .AnyAsync(
                x => x.Id == contentId,
                cancellationToken);

        if (!contentExists)
        {
            throw new KeyNotFoundException(
                $"Content with id '{contentId}' was not found.");
        }

        var watchStatus = await dbContext.WatchStatuses
            .FirstOrDefaultAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);

        if (watchStatus is null)
        {
            watchStatus = new WatchStatus
            {
                UserId = userId,
                ContentId = contentId,
                Status = status
            };

            await dbContext.WatchStatuses.AddAsync(
                watchStatus,
                cancellationToken);
        }
        else
        {
            watchStatus.Status = status;
        }

        if (status == WatchStatusType.Completed)
        {
            var watchlistItem = await dbContext.Watchlists
                .FirstOrDefaultAsync(
                    x => x.UserId == userId
                         && x.ContentId == contentId,
                    cancellationToken);

            if (watchlistItem is not null)
            {
                dbContext.Watchlists.Remove(watchlistItem);
            }
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<WatchStatusType?> GetAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.WatchStatuses
            .Where(x =>
                x.UserId == userId
                && x.ContentId == contentId)
            .Select(x => (WatchStatusType?)x.Status)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task RemoveAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        var watchStatus = await dbContext.WatchStatuses
            .FirstOrDefaultAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);

        if (watchStatus is null)
        {
            return;
        }

        dbContext.WatchStatuses.Remove(watchStatus);

        await dbContext.SaveChangesAsync(cancellationToken);
    }
}