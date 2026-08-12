using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Search;

/// <summary>
/// Represents paged multi-search results returned by TMDb.
/// </summary>
public sealed class MultiSearchResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<MultiSearchItemResponse> Results { get; init; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; init; }
}