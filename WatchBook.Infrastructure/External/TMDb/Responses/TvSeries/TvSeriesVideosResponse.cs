using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents TV series videos returned by TMDb.
/// </summary>
public sealed class TvSeriesVideosResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<TvSeriesVideoResponse> Results { get; init; } = [];
}