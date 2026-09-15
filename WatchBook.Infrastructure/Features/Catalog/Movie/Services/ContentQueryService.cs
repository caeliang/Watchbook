using Microsoft.EntityFrameworkCore;
using WatchBook.Application.Features;
using WatchBook.Application.Features.Catalog.DTOs;
using WatchBook.Application.Features.Catalog.Interfaces;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Features.Catalog.Movie.Services;

public sealed class ContentQueryService(
    WatchBookDbContext dbContext) : IContentQueryService
{
    public async Task<ContentDetailDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Contents
            .AsNoTracking()
            .Where(content => content.Id == id)
            .Select(content => new ContentDetailDto
            {
                Id = content.Id,
                TmdbId = content.TmdbId,
                Type = content.Type,
                Title = content.Title,
                OriginalTitle = content.OriginalTitle,
                Overview = content.Overview,
                PosterPath = content.PosterPath,
                BackdropPath = content.BackdropPath,
                ReleaseDate = content.ReleaseDate,
                Runtime = content.Runtime,
                Popularity = content.Popularity,
                VoteAverage = content.VoteAverage,
                VoteCount = content.VoteCount,
                Status = content.Status,
                ProductionStatus = content.ProductionStatus,

                Genres = content.ContentGenres
                    .Select(contentGenre => new GenreDto
                    {
                        Id = contentGenre.Genre.Id,
                        TmdbId = contentGenre.Genre.TmdbId,
                        Name = contentGenre.Genre.Name
                    })
                    .ToList(),

                Companies = content.ContentCompanies
                    .Select(contentCompany => new CompanyDto
                    {
                        Id = contentCompany.Company.Id,
                        TmdbId = contentCompany.Company.TmdbId,
                        Name = contentCompany.Company.Name,
                        Homepage = contentCompany.Company.Homepage,
                        LogoPath = contentCompany.Company.LogoPath,
                        OriginCountry = contentCompany.Company.OriginCountry
                    })
                    .ToList(),

                Countries = content.ContentCountries
                    .Select(contentCountry => new CountryDto
                    {
                        Code = contentCountry.Country.Code,
                        Name = contentCountry.Country.Name
                    })
                    .ToList(),

                People = content.ContentPeople
                    .Select(contentPerson => new PersonDto
                    {
                        Id = contentPerson.Person.Id,
                        TmdbId = contentPerson.Person.TmdbId,
                        Name = contentPerson.Person.Name,
                        OriginalName = contentPerson.Person.OriginalName,
                        ProfilePath = contentPerson.Person.ProfilePath,
                        Role = contentPerson.Role,
                        CharacterName = contentPerson.CharacterName,
                        DisplayOrder = contentPerson.DisplayOrder
                    })
                    .OrderBy(person => person.Role)
                    .ThenBy(person => person.DisplayOrder)
                    .ToList(),

                Seasons = content.Seasons
                    .OrderBy(season => season.SeasonNumber)
                    .Select(season => new SeasonDto
                    {
                        Id = season.Id,
                        TmdbId = season.TmdbId,
                        SeasonNumber = season.SeasonNumber,
                        Name = season.Name,
                        Overview = season.Overview,
                        PosterPath = season.PosterPath,
                        AirDate = season.AirDate,
                        EpisodeCount = season.EpisodeCount,

                        Episodes = season.Episodes
                            .OrderBy(episode => episode.EpisodeNumber)
                            .Select(episode => new EpisodeDto
                            {
                                Id = episode.Id,
                                TmdbId = episode.TmdbId,
                                EpisodeNumber = episode.EpisodeNumber,
                                Name = episode.Name,
                                Overview = episode.Overview,
                                AirDate = episode.AirDate,
                                Runtime = episode.Runtime,
                                StillPath = episode.StillPath,
                                VoteAverage = episode.VoteAverage,
                                VoteCount = episode.VoteCount
                            })
                            .ToList()
                    })
                    .ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
    }
}