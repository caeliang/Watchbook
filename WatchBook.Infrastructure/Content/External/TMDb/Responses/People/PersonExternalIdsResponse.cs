using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.People;

/// <summary>
/// Represents external IDs for a person.
/// </summary>
public sealed class PersonExternalIdsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; init; }

    [JsonPropertyName("facebook_id")]
    public string? FacebookId { get; init; }

    [JsonPropertyName("instagram_id")]
    public string? InstagramId { get; init; }

    [JsonPropertyName("twitter_id")]
    public string? TwitterId { get; init; }

    [JsonPropertyName("wikidata_id")]
    public string? WikidataId { get; init; }

    [JsonPropertyName("tiktok_id")]
    public string? TikTokId { get; init; }

    [JsonPropertyName("youtube_id")]
    public string? YouTubeId { get; init; }
}