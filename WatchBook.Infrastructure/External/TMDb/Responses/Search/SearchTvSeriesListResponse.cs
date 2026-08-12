using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Search;

/// <summary>
/// Represents paged TV series search results.
/// </summary>
public sealed class SearchTvSeriesListResponse
{
    [JsonPropertyName("page")]
    public int Page { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<SearchTvSeriesResponse> Results { get; init; } = [];

    [JsonPropertyName("total_pages")]
    public int TotalPages { get; init; }

    [JsonPropertyName("total_results")]
    public int TotalResults { get; init; }
}