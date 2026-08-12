using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Discover;

/// <summary>
/// Represents a discover result.
/// </summary>
public sealed class DiscoverItemResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("title")]
    public string? Title { get; init; }

    [JsonPropertyName("name")]
    public string? Name { get; init; }

    [JsonPropertyName("original_title")]
    public string? OriginalTitle { get; init; }

    [JsonPropertyName("original_name")]
    public string? OriginalName { get; init; }

    [JsonPropertyName("overview")]
    public string? Overview { get; init; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; init; }

    [JsonPropertyName("release_date")]
    public DateOnly? ReleaseDate { get; init; }

    [JsonPropertyName("first_air_date")]
    public DateOnly? FirstAirDate { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }
}