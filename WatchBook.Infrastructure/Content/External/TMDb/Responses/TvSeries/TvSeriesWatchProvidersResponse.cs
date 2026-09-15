using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents watch providers returned by TMDb for a TV series.
/// </summary>
public sealed class TvSeriesWatchProvidersResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("results")]
    public Dictionary<string, TvSeriesWatchProviderCountryResponse> Results { get; init; } = [];
}