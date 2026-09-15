using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents external IDs of a TV series.
/// </summary>
public sealed class TvExternalIdsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; init; }

    [JsonPropertyName("tvdb_id")]
    public int? TvdbId { get; init; }

    [JsonPropertyName("wikidata_id")]
    public string? WikidataId { get; init; }

    [JsonPropertyName("facebook_id")]
    public string? FacebookId { get; init; }

    [JsonPropertyName("instagram_id")]
    public string? InstagramId { get; init; }

    [JsonPropertyName("twitter_id")]
    public string? TwitterId { get; init; }
}