using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Domain.Enums.Content;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Mapping;
using WatchBook.Infrastructure.Persistence;
using WatchBook.Infrastructure.Services.Catalog;

namespace WatchBook.Infrastructure.Services.Import;

public sealed class MovieImportService(
    IMovieClient movieClient,
    GenreSyncService genreSyncService,
    CompanySyncService companySyncService,
    CountrySyncService countrySyncService,
    PersonSyncService personSyncService,
    WatchBookDbContext dbContext)
{
    public async Task<Content> ImportAsync(
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

        var movie = await movieClient.GetDetailsAsync(
            tmdbId,
            cancellationToken);

        var credits = await movieClient.GetCreditsAsync(
            tmdbId,
            cancellationToken);

        var content = MovieMapper.ToEntity(movie);

        foreach (var genreResponse in movie.Genres)
        {
            var genre = await genreSyncService.SyncAsync(
                genreResponse,
                cancellationToken);

            content.ContentGenres.Add(new ContentGenre
            {
                Genre = genre
            });
        }

        foreach (var companyResponse in movie.ProductionCompanies)
        {
            var company = await companySyncService.SyncAsync(
                companyResponse,
                cancellationToken);

            content.ContentCompanies.Add(new ContentCompany
            {
                Company = company
            });
        }

        foreach (var countryResponse in movie.ProductionCountries)
        {
            var country = await countrySyncService.SyncAsync(
                countryResponse,
                cancellationToken);

            content.ContentCountries.Add(new ContentCountry
            {
                Country = country
            });
        }

        foreach (var cast in credits.Cast)
        {
            var person = await personSyncService.SyncAsync(
                cast,
                cancellationToken);

            content.ContentPeople.Add(new ContentPerson
            {
                Person = person,
                Role = PersonRole.Actor,
                CharacterName = cast.Character,
                DisplayOrder = cast.Order
            });
        }

        dbContext.Contents.Add(content);

        return content;
    }
}