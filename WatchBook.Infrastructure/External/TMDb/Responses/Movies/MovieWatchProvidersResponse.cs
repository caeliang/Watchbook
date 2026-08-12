using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents watch providers returned by TMDb.
/// </summary>
public sealed class MovieWatchProvidersResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("results")]
    public Dictionary<string, MovieWatchProviderCountryResponse> Results { get; init; } = [];
}