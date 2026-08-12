using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents a cast member in movie credits.
/// </summary>
public sealed class MovieCastResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("original_name")]
    public string OriginalName { get; init; } = string.Empty;

    [JsonPropertyName("character")]
    public string? Character { get; init; }

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; init; }

    [JsonPropertyName("order")]
    public int Order { get; init; }
}