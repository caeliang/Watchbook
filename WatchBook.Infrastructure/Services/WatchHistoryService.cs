using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Interfaces;
using WatchBook.Domain.Entities.User;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services;

public sealed class WatchHistoryService(
    WatchBookDbContext dbContext) : IWatchHistoryService
{
    public async Task AddAsync(
        string userId,
        int contentId,
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

        var watchHistory = new WatchHistory
        {
            UserId = userId,
            ContentId = contentId,
            WatchedAt = DateTime.UtcNow
        };

        await dbContext.WatchHistories.AddAsync(
            watchHistory,
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(
        string userId,
        int historyId,
        CancellationToken cancellationToken = default)
    {
        var watchHistory = await dbContext.WatchHistories
            .FirstOrDefaultAsync(
                x => x.Id == historyId
                     && x.UserId == userId,
                cancellationToken);

        if (watchHistory is null)
        {
            return;
        }

        dbContext.WatchHistories.Remove(watchHistory);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<WatchHistoryItem>> GetAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.WatchHistories
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.WatchedAt)
            .Select(x => new WatchHistoryItem(
                x.Id,
                x.ContentId,
                x.Content.Title,
                x.WatchedAt))
            .ToListAsync(cancellationToken);
    }
}