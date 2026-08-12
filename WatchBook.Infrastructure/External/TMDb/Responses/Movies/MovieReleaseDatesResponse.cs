using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents movie release dates returned by TMDb.
/// </summary>
public sealed class MovieReleaseDatesResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<MovieReleaseCountryResponse> Results { get; init; } = [];
}