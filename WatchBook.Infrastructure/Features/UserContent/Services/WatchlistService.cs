using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Features.UserContent.Services;
using WatchBook.Domain.Features.UserContent.Entities;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Features.UserContent.Services;

public sealed class WatchlistService(
    WatchBookDbContext dbContext) : IWatchlistService
{
    public async Task AddAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.Watchlists
            .AnyAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);

        if (exists)
        {
            return;
        }

        var contentExists = await dbContext.Contents
            .AnyAsync(
                x => x.Id == contentId,
                cancellationToken);

        if (!contentExists)
        {
            throw new KeyNotFoundException(
                $"Content with id '{contentId}' was not found.");
        }

        var watchlist = new Watchlist
        {
            UserId = userId,
            ContentId = contentId
        };

        await dbContext.Watchlists.AddAsync(
            watchlist,
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        var watchlist = await dbContext.Watchlists
            .FirstOrDefaultAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);

        if (watchlist is null)
        {
            return;
        }

        dbContext.Watchlists.Remove(watchlist);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Watchlists
            .AnyAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);
    }
}