using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Enums.Content;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;

namespace WatchBook.Infrastructure.External.TMDb.Mapping;

/// <summary>
/// Maps TMDb movie responses into domain content entities.
/// </summary>
public static class MovieMapper
{
    public static Content ToEntity(MovieDetailsResponse response)
    {
        ArgumentNullException.ThrowIfNull(response);

        return new Content
        {
            TmdbId = response.Id,

            Type = ContentType.Movie,

            Title = response.Title,

            OriginalTitle = response.OriginalTitle,

            Overview = response.Overview,

            PosterPath = response.PosterPath,

            BackdropPath = response.BackdropPath,

            ReleaseDate = response.ReleaseDate,

            Runtime = response.Runtime,

            Popularity = response.Popularity,

            VoteAverage = response.VoteAverage,

            VoteCount = response.VoteCount,

            Status = ContentStatus.Active,

            ProductionStatus = MapProductionStatus(response.Status)
        };
    }

    private static ProductionStatus MapProductionStatus(string? status)
    {
        if (string.IsNullOrWhiteSpace(status))
        {
            return ProductionStatus.Unknown;
        }

        return status switch
        {
            "Rumored" => ProductionStatus.Rumored,
            "Planned" => ProductionStatus.Planned,
            "In Production" => ProductionStatus.InProduction,
            "Post Production" => ProductionStatus.PostProduction,
            "Released" => ProductionStatus.Released,
            "Canceled" => ProductionStatus.Canceled,
            _ => ProductionStatus.Unknown
        };
    }
}