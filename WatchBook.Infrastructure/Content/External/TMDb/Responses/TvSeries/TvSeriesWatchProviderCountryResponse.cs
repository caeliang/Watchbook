using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents streaming providers for a country.
/// </summary>
public sealed class TvSeriesWatchProviderCountryResponse
{
    [JsonPropertyName("link")]
    public string? Link { get; init; }

    [JsonPropertyName("flatrate")]
    public IReadOnlyList<TvSeriesWatchProviderResponse> Flatrate { get; init; } = [];

    [JsonPropertyName("rent")]
    public IReadOnlyList<TvSeriesWatchProviderResponse> Rent { get; init; } = [];

    [JsonPropertyName("buy")]
    public IReadOnlyList<TvSeriesWatchProviderResponse> Buy { get; init; } = [];
}