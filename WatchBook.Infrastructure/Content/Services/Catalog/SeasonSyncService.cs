using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

public sealed class SeasonSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public SeasonSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Season> SyncAsync(
        Content content,
        TvSeasonDetailsResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(response);

        var existingSeason = await _dbContext.Seasons
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (existingSeason is not null)
        {
            return existingSeason;
        }

        var season = new Season
        {
            TmdbId = response.Id,
            Content = content,
            SeasonNumber = response.SeasonNumber,
            Name = response.Name,
            Overview = response.Overview,
            PosterPath = response.PosterPath,
            AirDate = DateOnly.TryParse(
                response.AirDate,
                out var airDate)
                    ? airDate
                    : null,
            EpisodeCount = response.Episodes.Count
        };

        await _dbContext.Seasons.AddAsync(
            season,
            cancellationToken);

        return season;
    }
}