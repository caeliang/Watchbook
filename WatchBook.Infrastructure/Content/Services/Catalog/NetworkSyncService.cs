using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.Common;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

/// <summary>
/// Synchronizes television network entities from TMDb responses.
/// Implements idempotent sync pattern: looks up network by TmdbId,
/// creates if not found, updates properties, and persists to database.
/// </summary>
public sealed class NetworkSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public NetworkSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Synchronizes a network entity from TMDb response.
    /// Returns existing network if found by TmdbId, otherwise creates new one.
    /// </summary>
    /// <param name="response">The TMDb network response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The synchronized network entity.</returns>
    public async Task<Network> SyncAsync(
        NetworkResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var existingNetwork = await _dbContext.Networks
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (existingNetwork is not null)
        {
            return existingNetwork;
        }

        var network = new Network
        {
            TmdbId = response.Id,
            Name = response.Name,
            LogoPath = response.LogoPath,
            OriginCountry = response.OriginCountry,
            IsActive = true
        };

        await _dbContext.Networks.AddAsync(
            network,
            cancellationToken);

        return network;
    }
}
