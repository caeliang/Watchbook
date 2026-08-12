using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;

namespace WatchBook.Infrastructure.External.TMDb.Mapping;

public static class PersonMapper
{
    public static Person FromCast(
        MovieCastResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new Person
        {
            TmdbId = response.Id,
            Name = response.Name,
            OriginalName = response.OriginalName,
            ProfilePath = response.ProfilePath
        };
    }


    public static Person FromCrew(
        MovieCrewResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new Person
        {
            TmdbId = response.Id,
            Name = response.Name,
            OriginalName = response.OriginalName,
            ProfilePath = response.ProfilePath
        };
    }
}