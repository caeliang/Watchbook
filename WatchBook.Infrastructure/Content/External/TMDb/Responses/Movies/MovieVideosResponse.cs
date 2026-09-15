using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents movie videos returned by TMDb.
/// </summary>
public sealed class MovieVideosResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("results")]
    public IReadOnlyList<MovieVideoResponse> Results { get; init; } = [];
}