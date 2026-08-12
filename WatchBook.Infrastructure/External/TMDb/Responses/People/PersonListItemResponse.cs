using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.People;

/// <summary>
/// Represents a person in a paged list.
/// </summary>
public sealed class PersonListItemResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; init; }

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }
}