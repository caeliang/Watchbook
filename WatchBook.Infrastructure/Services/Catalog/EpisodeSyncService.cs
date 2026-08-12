using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

/// <summary>
/// Synchronizes episode entities from TMDb responses.
/// Implements idempotent sync pattern: looks up episode by TmdbId,
/// creates if not found, updates properties, and persists to database.
/// </summary>
public sealed class EpisodeSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public EpisodeSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Synchronizes an episode entity from TMDb response.
    /// Returns existing episode if found by TmdbId, otherwise creates new one.
    /// </summary>
    /// <param name="seasonId">The ID of the season this episode belongs to.</param>
    /// <param name="response">The TMDb episode response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The synchronized episode entity.</returns>
    public async Task<Episode> SyncAsync(
        int seasonId,
        TvEpisodeResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var existingEpisode = await _dbContext.Episodes
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (existingEpisode is not null)
        {
            return existingEpisode;
        }

        var episode = new Episode
        {
            SeasonId = seasonId,
            TmdbId = response.Id,
            EpisodeNumber = response.EpisodeNumber,
            Name = response.Name,
            Overview = response.Overview,
            AirDate = response.AirDate,
            Runtime = response.Runtime,
            StillPath = response.StillPath,
            VoteAverage = response.VoteAverage,
            VoteCount = response.VoteCount
        };

        await _dbContext.Episodes.AddAsync(
            episode,
            cancellationToken);

        await _dbContext.SaveChangesAsync(
            cancellationToken);

        return episode;
    }
}
