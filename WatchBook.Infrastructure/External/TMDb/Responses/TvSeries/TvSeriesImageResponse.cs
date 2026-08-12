using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents an image returned by TMDb for a TV series.
/// </summary>
public sealed class TvSeriesImageResponse
{
    [JsonPropertyName("aspect_ratio")]
    public double AspectRatio { get; init; }

    [JsonPropertyName("height")]
    public int Height { get; init; }

    [JsonPropertyName("width")]
    public int Width { get; init; }

    [JsonPropertyName("file_path")]
    public string FilePath { get; init; } = string.Empty;

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }

    [JsonPropertyName("iso_639_1")]
    public string? Language { get; init; }
}