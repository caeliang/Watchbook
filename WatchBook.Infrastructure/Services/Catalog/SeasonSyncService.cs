using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

/// <summary>
/// Synchronizes season entities from TMDb responses.
/// Implements idempotent sync pattern: looks up season by TmdbId,
/// creates if not found, updates properties, and persists to database.
/// </summary>
public sealed class SeasonSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public SeasonSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Synchronizes a season entity from TMDb response.
    /// Returns existing season if found by TmdbId, otherwise creates new one.
    /// </summary>
    /// <param name="contentId">The ID of the content (TV series) this season belongs to.</param>
    /// <param name="response">The TMDb season response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The synchronized season entity.</returns>
    public async Task<Season> SyncAsync(
        int contentId,
        TvSeasonResponse response,
        CancellationToken cancellationToken = default)
    {
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
            ContentId = contentId,
            TmdbId = response.Id,
            SeasonNumber = response.SeasonNumber,
            Name = response.Name,
            Overview = response.Overview,
            PosterPath = response.PosterPath,
            AirDate = response.AirDate,
            EpisodeCount = response.Episodes?.Count ?? 0
        };

        await _dbContext.Seasons.AddAsync(
            season,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return season;
    }
}
