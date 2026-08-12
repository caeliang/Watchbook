using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Common;

/// <summary>
/// Represents a TMDb genre.
/// </summary>
public sealed class GenreResponse
{
    /// <summary>
    /// Gets the TMDb genre identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// Gets the genre name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}