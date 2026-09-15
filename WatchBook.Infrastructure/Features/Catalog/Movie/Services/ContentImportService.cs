
using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Features.Catalog.Entities;
using WatchBook.Infrastructure.Features.Catalog.Movie.Interfaces;
using WatchBook.Infrastructure.Features.Catalog.TvSeries.Services;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Features.Catalog.Movie.Services;

public sealed class ContentImportService(
    MovieImportService movieImportService,
    TvSeriesImportService tvSeriesImportService,
    WatchBookDbContext dbContext) : IContentImportService
{
    public async Task<Content> ImportMovieAsync(
        int tmdbId,
        CancellationToken cancellationToken = default)
    {
        var existingContent = await dbContext.Contents
            .FirstOrDefaultAsync(
                x => x.TmdbId == tmdbId,
                cancellationToken);

        if (existingContent is not null)
        {
            return existingContent;
        }

        await using var transaction =
            await dbContext.Database.BeginTransactionAsync(
                cancellationToken);

        try
        {
            var content = await movieImportService.ImportAsync(
                tmdbId,
                cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return content;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }

    public async Task<Content> ImportTvSeriesAsync(
        int tmdbId,
        CancellationToken cancellationToken = default)
    {
        await using var transaction =
            await dbContext.Database.BeginTransactionAsync(
                cancellationToken);

        try
        {
            var content = await tvSeriesImportService.ImportAsync(
                tmdbId,
                cancellationToken);

            await dbContext.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return content;
        }
        catch
        {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}
