using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

public sealed class EpisodeSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public EpisodeSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Episode> SyncAsync(
        Season season,
        TvEpisodeResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(season);
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
            TmdbId = response.Id,
            Season = season,
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

        return episode;
    }
}