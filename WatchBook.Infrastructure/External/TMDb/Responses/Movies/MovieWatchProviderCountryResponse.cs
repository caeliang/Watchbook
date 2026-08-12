using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents streaming providers for a country.
/// </summary>
public sealed class MovieWatchProviderCountryResponse
{
    [JsonPropertyName("link")]
    public string? Link { get; init; }

    [JsonPropertyName("flatrate")]
    public IReadOnlyList<MovieWatchProviderResponse> Flatrate { get; init; } = [];

    [JsonPropertyName("rent")]
    public IReadOnlyList<MovieWatchProviderResponse> Rent { get; init; } = [];

    [JsonPropertyName("buy")]
    public IReadOnlyList<MovieWatchProviderResponse> Buy { get; init; } = [];
}