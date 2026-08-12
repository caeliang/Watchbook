using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.People;

/// <summary>
/// Represents a movie credit for a person.
/// </summary>
public sealed class PersonMovieCreditResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; init; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; init; } = string.Empty;

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; init; }

    [JsonPropertyName("release_date")]
    public DateOnly? ReleaseDate { get; init; }

    [JsonPropertyName("character")]
    public string? Character { get; init; }

    [JsonPropertyName("job")]
    public string? Job { get; init; }

    [JsonPropertyName("department")]
    public string? Department { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }
}