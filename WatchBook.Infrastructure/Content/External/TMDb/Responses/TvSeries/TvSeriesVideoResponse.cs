using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents a video returned by TMDb for a TV series.
/// </summary>
public sealed class TvSeriesVideoResponse
{
    [JsonPropertyName("id")]
    public string Id { get; init; } = string.Empty;

    [JsonPropertyName("key")]
    public string Key { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("site")]
    public string Site { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("official")]
    public bool Official { get; init; }

    [JsonPropertyName("published_at")]
    public DateTimeOffset PublishedAt { get; init; }
}