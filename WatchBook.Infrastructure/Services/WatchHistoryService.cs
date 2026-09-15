using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Interfaces;
using WatchBook.Domain.Features.UserContent.Entities;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services;

public sealed class WatchHistoryService(
    WatchBookDbContext dbContext) : IWatchHistoryService
{
    public async Task AddAsync(
        string userId,
        int contentId,
        int? episodeId,
        decimal? rating,
        CancellationToken cancellationToken = default)
    {
        if (rating.HasValue)
        {
            if (rating.Value < 0.5m || rating.Value > 5.0m)
            {
                throw new ArgumentException(
                    "Rating must be between 0.5 and 5.0.");
            }

            if (rating.Value % 0.5m != 0)
            {
                throw new ArgumentException(
                    "Rating must be in increments of 0.5.");
            }
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

        if (episodeId.HasValue)
        {
            var episodeBelongsToContent = await dbContext.Episodes
                .AnyAsync(
                    x => x.Id == episodeId.Value
                         && x.Season.ContentId == contentId,
                    cancellationToken);

            if (!episodeBelongsToContent)
            {
                throw new KeyNotFoundException(
                    $"Episode with id '{episodeId.Value}' was not found for content '{contentId}'.");
            }
        }

        var watchHistory = new WatchHistory
        {
            UserId = userId,
            ContentId = contentId,
            EpisodeId = episodeId,
            WatchedAt = DateTime.UtcNow,
            Rating = rating
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
                x.EpisodeId,
                x.Content.Title,
                x.Episode != null
                    ? x.Episode.Season.SeasonNumber
                    : null,
                x.Episode != null
                    ? x.Episode.EpisodeNumber
                    : null,
                x.Episode != null
                    ? x.Episode.Name
                    : null,
                x.WatchedAt,
                x.Rating))
            .ToListAsync(cancellationToken);
    }
}