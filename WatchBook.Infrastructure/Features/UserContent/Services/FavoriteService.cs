using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Interfaces;
using WatchBook.Infrastructure.Persistence;
using WatchBook.Domain.Features.UserContent.Entities;
namespace WatchBook.Infrastructure.Features.UserContent.Services;

public sealed class FavoriteService(
    WatchBookDbContext dbContext) : IFavoriteService
{
    public async Task AddAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        var exists = await dbContext.Favorites
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

        var favorite = new Favorite
        {
            UserId = userId,
            ContentId = contentId
        };

        await dbContext.Favorites.AddAsync(
            favorite,
            cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        var favorite = await dbContext.Favorites
            .FirstOrDefaultAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);

        if (favorite is null)
        {
            return;
        }

        dbContext.Favorites.Remove(favorite);

        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<bool> ExistsAsync(
        string userId,
        int contentId,
        CancellationToken cancellationToken = default)
    {
        return dbContext.Favorites
            .AnyAsync(
                x => x.UserId == userId
                     && x.ContentId == contentId,
                cancellationToken);
    }
}