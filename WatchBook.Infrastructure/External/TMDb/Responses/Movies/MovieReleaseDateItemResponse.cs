using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents a movie release date.
/// </summary>
public sealed class MovieReleaseDateItemResponse
{
    [JsonPropertyName("certification")]
    public string? Certification { get; init; }

    [JsonPropertyName("iso_639_1")]
    public string? Language { get; init; }

    [JsonPropertyName("release_date")]
    public DateTimeOffset ReleaseDate { get; init; }

    [JsonPropertyName("type")]
    public int Type { get; init; }
}