using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents the credits of a TV series.
/// </summary>
public sealed class TvSeriesCreditsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("cast")]
    public IReadOnlyList<TvSeriesCastResponse> Cast { get; init; } = [];

    [JsonPropertyName("crew")]
    public IReadOnlyList<TvSeriesCrewResponse> Crew { get; init; } = [];
}