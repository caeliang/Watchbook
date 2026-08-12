using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.Common;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

public sealed class GenreSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public GenreSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Genre> SyncAsync(
        GenreResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);


        var existingGenre = await _dbContext.Genres
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);


        if (existingGenre is not null)
        {
            return existingGenre;
        }


        var genre = new Genre
        {
            TmdbId = response.Id,
            Name = response.Name,
            IsActive = true
        };


        await _dbContext.Genres.AddAsync(
            genre,
            cancellationToken);


        await _dbContext.SaveChangesAsync(
            cancellationToken);


        return genre;
    }
}