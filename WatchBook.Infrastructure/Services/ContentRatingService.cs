using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Interfaces;
using WatchBook.Domain.Features.Catalog.Enums;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services;

public sealed class ContentRatingService(
    WatchBookDbContext dbContext) : IContentRatingService
{
    public async Task<ContentRatingSummary?> GetAsync(
        int contentId,
        CancellationToken cancellationToken = default)
    {
        var content = await dbContext.Contents
            .AsNoTracking()
            .Where(x => x.Id == contentId)
            .Select(x => new
            {
                x.Id,
                x.Type
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (content is null)
        {
            return null;
        }

        var ratedHistory = await dbContext.WatchHistories
            .AsNoTracking()
            .Where(x =>
                x.ContentId == contentId
                && x.Rating.HasValue)
            .Select(x => new
            {
                x.EpisodeId,
                EpisodeNumber = x.Episode != null
                    ? x.Episode.EpisodeNumber
                    : (int?)null,
                EpisodeName = x.Episode != null
                    ? x.Episode.Name
                    : null,
                SeasonId = x.Episode != null
                    ? x.Episode.SeasonId
                    : (int?)null,
                SeasonNumber = x.Episode != null
                    ? x.Episode.Season.SeasonNumber
                    : (int?)null,
                Rating = x.Rating!.Value
            })
            .ToListAsync(cancellationToken);

        if (content.Type == ContentType.Movie)
        {
            decimal? movieRating = ratedHistory.Count == 0
                ? null
                : ratedHistory.Average(x => x.Rating);

            return new ContentRatingSummary(
                content.Id,
                movieRating,
                []);
        }

        var episodeRatings = ratedHistory
            .Where(x =>
                x.EpisodeId.HasValue
                && x.SeasonId.HasValue
                && x.EpisodeNumber.HasValue
                && x.SeasonNumber.HasValue
                && x.EpisodeName is not null)
            .GroupBy(x => new
            {
                x.EpisodeId,
                x.EpisodeNumber,
                x.EpisodeName,
                x.SeasonId,
                x.SeasonNumber
            })
            .Select(group => new EpisodeRatingSummary(
                group.Key.EpisodeId!.Value,
                group.Key.EpisodeNumber!.Value,
                group.Key.EpisodeName!,
                group.Average(x => x.Rating)))
            .OrderBy(x => x.EpisodeNumber)
            .ToList();

        var seasonRatings = episodeRatings
            .Join(
                ratedHistory
                    .Where(x =>
                        x.EpisodeId.HasValue
                        && x.SeasonId.HasValue
                        && x.SeasonNumber.HasValue)
                    .GroupBy(x => new
                    {
                        x.EpisodeId,
                        x.SeasonId,
                        x.SeasonNumber
                    })
                    .Select(group => new
                    {
                        EpisodeId = group.Key.EpisodeId!.Value,
                        SeasonId = group.Key.SeasonId!.Value,
                        SeasonNumber = group.Key.SeasonNumber!.Value
                    }),
                episode => episode.EpisodeId,
                episode => episode.EpisodeId,
                (episode, metadata) => new
                {
                    metadata.SeasonId,
                    metadata.SeasonNumber,
                    Episode = episode
                })
            .GroupBy(x => new
            {
                x.SeasonId,
                x.SeasonNumber
            })
            .Select(group =>
            {
                var episodes = group
                    .Select(x => x.Episode)
                    .OrderBy(x => x.EpisodeNumber)
                    .ToList();

                return new SeasonRatingSummary(
                    group.Key.SeasonId,
                    group.Key.SeasonNumber,
                    episodes.Average(x => x.Rating),
                    episodes);
            })
            .OrderBy(x => x.SeasonNumber)
            .ToList();

        decimal? seriesRating = seasonRatings.Count == 0
            ? null
            : seasonRatings.Average(x => x.Rating);

        return new ContentRatingSummary(
            content.Id,
            seriesRating,
            seasonRatings);
    }
}