using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Trending;

/// <summary>
/// Represents a paged trending response.
/// </summary>
public sealed class TrendingResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<TrendingItemResponse> Results { get; init; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; init; }
}