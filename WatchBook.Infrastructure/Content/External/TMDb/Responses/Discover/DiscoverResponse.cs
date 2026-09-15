using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Discover;

/// <summary>
/// Represents a paged discover response.
/// </summary>
public sealed class DiscoverResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<DiscoverItemResponse> Results { get; init; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; init; }
}