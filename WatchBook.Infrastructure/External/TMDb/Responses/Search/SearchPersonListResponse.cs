using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Search;

/// <summary>
/// Represents paged person search results.
/// </summary>
public sealed class SearchPersonListResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<SearchPersonResponse> Results { get; init; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; init; }
}