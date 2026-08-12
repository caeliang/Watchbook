using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents a TV episode returned by TMDb.
/// </summary>
public sealed class TvEpisodeResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("episode_number")]
    public int EpisodeNumber { get; init; }

    [JsonPropertyName("season_number")]
    public int SeasonNumber { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; init; } = string.Empty;

    [JsonPropertyName("runtime")]
    public int? Runtime { get; init; }

    [JsonPropertyName("air_date")]
    public DateOnly? AirDate { get; init; }

    [JsonPropertyName("still_path")]
    public string? StillPath { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }
}