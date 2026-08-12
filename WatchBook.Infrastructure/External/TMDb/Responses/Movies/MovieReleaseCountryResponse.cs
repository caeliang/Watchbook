using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents release dates for a country.
/// </summary>
public sealed class MovieReleaseCountryResponse
{
    [JsonPropertyName("iso_3166_1")]
    public string CountryCode { get; init; } = string.Empty;

    [JsonPropertyName("release_dates")]
    public IReadOnlyList<MovieReleaseDateItemResponse> ReleaseDates { get; init; } = [];
}