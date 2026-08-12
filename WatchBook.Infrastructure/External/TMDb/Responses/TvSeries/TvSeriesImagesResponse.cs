using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents TV series images returned by TMDb.
/// </summary>
public sealed class TvSeriesImagesResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("backdrops")]
    public IReadOnlyList<TvSeriesImageResponse> Backdrops { get; init; } = [];

    [JsonPropertyName("posters")]
    public IReadOnlyList<TvSeriesImageResponse> Posters { get; init; } = [];

    [JsonPropertyName("logos")]
    public IReadOnlyList<TvSeriesImageResponse> Logos { get; init; } = [];
}