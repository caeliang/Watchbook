using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Domain.Enums.Content;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Mapping;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;
using WatchBook.Infrastructure.Persistence;
using WatchBook.Infrastructure.Services.Catalog;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Infrastructure.Services;

/// <summary>
/// Imports TMDb content into domain entities and persists the result.
/// Orchestrates the import workflow by retrieving TMDb data and delegating
/// related entity synchronization to specialized sync services.
/// </summary>
public sealed class ContentImportService(
    IMovieClient movieClient,
    ITvSeriesClient tvSeriesClient,
    GenreSyncService genreSyncService,
    CompanySyncService companySyncService,
    CountrySyncService countrySyncService,
    NetworkSyncService networkSyncService,
    PersonSyncService personSyncService,
    WatchBookDbContext dbContext) : IContentImportService
{
    private readonly ITvSeriesClient _tvSeriesClient = tvSeriesClient;
    private readonly IMovieClient _movieClient = movieClient;
    private readonly WatchBookDbContext _dbContext = dbContext;

    /// <summary>
    /// Imports a movie from TMDb using its TMDb identifier.
    /// </summary>
    public async Task<Content> ImportMovieAsync(
        int tmdbId,
        CancellationToken cancellationToken = default)
    {
        var existingContent = await _dbContext.Contents
            .FirstOrDefaultAsync(
                x => x.TmdbId == tmdbId,
                cancellationToken);

        if (existingContent is not null)
        {
            return existingContent;
        }

        var movie = await _movieClient.GetDetailsAsync(
            tmdbId,
            cancellationToken);

        var credits = await _movieClient.GetCreditsAsync(
            tmdbId,
            cancellationToken);

        var content = MovieMapper.ToEntity(movie);

        //-----------------------------------------
        // Genres
        //-----------------------------------------

        foreach (var genreResponse in movie.Genres)
        {
            var genre = await genreSyncService.SyncAsync(
                genreResponse,
                cancellationToken);

            content.ContentGenres.Add(
                new ContentGenre
                {
                    Genre = genre
                });
        }

        //-----------------------------------------
        // Companies
        //-----------------------------------------

        foreach (var companyResponse in movie.ProductionCompanies)
        {
            var company = await companySyncService.SyncAsync(
                companyResponse,
                cancellationToken);

            content.ContentCompanies.Add(
                new ContentCompany
                {
                    Company = company
                });
        }

        //-----------------------------------------
        // Countries
        //-----------------------------------------

        foreach (var countryResponse in movie.ProductionCountries)
        {
            var country = await countrySyncService.SyncAsync(
                countryResponse,
                cancellationToken);

            content.ContentCountries.Add(
                new ContentCountry
                {
                    Country = country
                });
        }

        //-----------------------------------------
        // Cast
        //-----------------------------------------

        foreach (var cast in credits.Cast)
        {
            var person = await personSyncService.SyncAsync(
                cast,
                cancellationToken);

            content.ContentPeople.Add(
                new ContentPerson
                {
                    Person = person,
                    Role = PersonRole.Actor,
                    CharacterName = cast.Character,
                    DisplayOrder = cast.Order
                });
        }

        //-----------------------------------------
        // Save
        //-----------------------------------------

        _dbContext.Contents.Add(content);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return content;
    }

    /// <summary>
    /// Imports a TV series from a TMDb response.
    /// </summary>
    public async Task<Content> ImportTvSeriesAsync(
        TvSeriesDetailsResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var existingContent = await _dbContext.Contents
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (existingContent is not null)
        {
            return existingContent;
        }

        var content = TvSeriesMapper.ToEntity(response);

        //-----------------------------------------
        // Genres
        //-----------------------------------------

        foreach (var genreResponse in response.Genres)
        {
            var genre = await genreSyncService.SyncAsync(
                genreResponse,
                cancellationToken);

            content.ContentGenres.Add(
                new ContentGenre
                {
                    Genre = genre
                });
        }

        //-----------------------------------------
        // Companies
        //-----------------------------------------

        foreach (var companyResponse in response.ProductionCompanies)
        {
            var company = await companySyncService.SyncAsync(
                companyResponse,
                cancellationToken);

            content.ContentCompanies.Add(
                new ContentCompany
                {
                    Company = company
                });
        }

        //-----------------------------------------
        // Countries
        //-----------------------------------------

        foreach (var countryResponse in response.ProductionCountries)
        {
            var country = await countrySyncService.SyncAsync(
                countryResponse,
                cancellationToken);

            content.ContentCountries.Add(
                new ContentCountry
                {
                    Country = country
                });
        }

        //-----------------------------------------
        // Networks
        //-----------------------------------------

        foreach (var networkResponse in response.Networks)
        {
            var network = await networkSyncService.SyncAsync(
                networkResponse,
                cancellationToken);

            content.ContentNetworks.Add(
                new ContentNetwork
                {
                    Network = network
                });
        }

        //-----------------------------------------
        // Save
        //-----------------------------------------

        _dbContext.Contents.Add(content);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return content;
    }
}