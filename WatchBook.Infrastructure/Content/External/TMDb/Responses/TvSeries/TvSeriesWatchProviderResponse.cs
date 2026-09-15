using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents a streaming provider for a TV series.
/// </summary>
public sealed class TvSeriesWatchProviderResponse
{
    [JsonPropertyName("provider_id")]
    public int ProviderId { get; init; }

    [JsonPropertyName("provider_name")]
    public string ProviderName { get; init; } = string.Empty;

    [JsonPropertyName("logo_path")]
    public string? LogoPath { get; init; }

    [JsonPropertyName("display_priority")]
    public int DisplayPriority { get; init; }
}