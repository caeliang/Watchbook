using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents movie images returned by TMDb.
/// </summary>
public sealed class MovieImagesResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("backdrops")]
    public IReadOnlyList<MovieImageResponse> Backdrops { get; init; } = [];

    [JsonPropertyName("posters")]
    public IReadOnlyList<MovieImageResponse> Posters { get; init; } = [];

    [JsonPropertyName("logos")]
    public IReadOnlyList<MovieImageResponse> Logos { get; init; } = [];
}