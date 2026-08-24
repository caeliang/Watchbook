using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Mapping;
using WatchBook.Infrastructure.Persistence;
using WatchBook.Infrastructure.Services.Catalog;

namespace WatchBook.Infrastructure.Services.Import;

public sealed class TvSeriesImportService(
    ITvSeriesClient tvSeriesClient,
    GenreSyncService genreSyncService,
    CompanySyncService companySyncService,
    CountrySyncService countrySyncService,
    NetworkSyncService networkSyncService,
    SeasonSyncService seasonSyncService,
    EpisodeSyncService episodeSyncService,
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

        var response = await tvSeriesClient.GetDetailsAsync(
            tmdbId,
            cancellationToken);

        var content = TvSeriesMapper.ToEntity(response);

        foreach (var genreResponse in response.Genres)
        {
            var genre = await genreSyncService.SyncAsync(
                genreResponse,
                cancellationToken);

            content.ContentGenres.Add(new ContentGenre
            {
                Genre = genre
            });
        }

        foreach (var companyResponse in response.ProductionCompanies)
        {
            var company = await companySyncService.SyncAsync(
                companyResponse,
                cancellationToken);

            content.ContentCompanies.Add(new ContentCompany
            {
                Company = company
            });
        }

        foreach (var countryResponse in response.ProductionCountries)
        {
            var country = await countrySyncService.SyncAsync(
                countryResponse,
                cancellationToken);

            content.ContentCountries.Add(new ContentCountry
            {
                Country = country
            });
        }

        foreach (var networkResponse in response.Networks)
        {
            var network = await networkSyncService.SyncAsync(
                networkResponse,
                cancellationToken);

            content.ContentNetworks.Add(new ContentNetwork
            {
                Network = network
            });
        }

        foreach (var seasonNumber in Enumerable.Range(
                     1,
                     response.NumberOfSeasons))
        {
            var seasonResponse = await tvSeriesClient.GetSeasonDetailsAsync(
                response.Id,
                seasonNumber,
                cancellationToken);

            var season = await seasonSyncService.SyncAsync(
                content,
                seasonResponse,
                cancellationToken);

            content.Seasons.Add(season);

            foreach (var episodeResponse in seasonResponse.Episodes)
            {
                var episode = await episodeSyncService.SyncAsync(
                    season,
                    episodeResponse,
                    cancellationToken);

                season.Episodes.Add(episode);
            }
        }

        dbContext.Contents.Add(content);

        return content;
    }
}