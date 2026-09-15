using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Search;

/// <summary>
/// Represents a person returned from a TMDb search.
/// </summary>
public sealed class SearchPersonResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("original_name")]
    public string OriginalName { get; init; } = string.Empty;

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }

    [JsonPropertyName("gender")]
    public int? Gender { get; init; }

    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; init; }

    [JsonPropertyName("known_for")]
    public IReadOnlyList<object> KnownFor { get; init; } = [];
}