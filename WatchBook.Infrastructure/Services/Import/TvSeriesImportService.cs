using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Features.Catalog.Entities;
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

        var isNewContent = existingContent is null;

        var response = await tvSeriesClient.GetDetailsAsync(
            tmdbId,
            cancellationToken);

        var content = existingContent
            ?? TvSeriesMapper.ToEntity(response);

        if (isNewContent)
        {
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
        }

        var existingSeasonTmdbIds = isNewContent
            ? []
            : await dbContext.Seasons
                .Where(season => season.ContentId == content.Id)
                .Select(season => season.TmdbId)
                .ToHashSetAsync(cancellationToken);

        for (var seasonNumber = 1;
             seasonNumber <= response.NumberOfSeasons;
             seasonNumber++)
        {
            var seasonResponse = await tvSeriesClient.GetSeasonDetailsAsync(
                response.Id,
                seasonNumber,
                cancellationToken);

            Season season;

            if (existingSeasonTmdbIds.Contains(seasonResponse.Id))
            {
                season = await dbContext.Seasons
                    .FirstAsync(
                        x => x.ContentId == content.Id
                             && x.TmdbId == seasonResponse.Id,
                        cancellationToken);
            }
            else
            {
                season = await seasonSyncService.SyncAsync(
                    content,
                    seasonResponse,
                    cancellationToken);

                content.Seasons.Add(season);

                existingSeasonTmdbIds.Add(seasonResponse.Id);
            }

            foreach (var episodeResponse in seasonResponse.Episodes)
            {
                var episode = await episodeSyncService.SyncAsync(
                    season,
                    episodeResponse,
                    cancellationToken);

                if (episode.Season != season)
                {
                    season.Episodes.Add(episode);
                }
            }
        }

        if (isNewContent)
        {
            dbContext.Contents.Add(content);
        }

        return content;
    }
}