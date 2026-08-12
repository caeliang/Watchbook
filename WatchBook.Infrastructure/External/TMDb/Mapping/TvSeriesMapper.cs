using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Enums.Content;
using WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

namespace WatchBook.Infrastructure.External.TMDb.Mapping;

public static class TvSeriesMapper
{
    public static Content ToEntity(
        TvSeriesDetailsResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new Content
        {
            TmdbId = response.Id,

            Type = ContentType.Series,

            Title = response.Name,

            OriginalTitle = response.OriginalName,

            Overview = response.Overview,

            PosterPath = response.PosterPath,

            BackdropPath = response.BackdropPath,

            ReleaseDate = response.FirstAirDate,

            Popularity = response.Popularity,

            VoteAverage = response.VoteAverage,

            VoteCount = response.VoteCount
        };
    }
}