using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents a TV season returned by TMDb.
/// </summary>
public sealed class TvSeasonResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; init; } = string.Empty;

    [JsonPropertyName("season_number")]
    public int SeasonNumber { get; init; }

    [JsonPropertyName("air_date")]
    public DateOnly? AirDate { get; init; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("episodes")]
    public IReadOnlyList<TvEpisodeResponse> Episodes { get; init; } = [];

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }
}