using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Common;

/// <summary>
/// Represents a person returned by TMDb.
/// </summary>
public sealed class PersonResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("original_name")]
    public string OriginalName { get; init; } = string.Empty;

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; init; }

    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }
}