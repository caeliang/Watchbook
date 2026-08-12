using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.People;

/// <summary>
/// Represents a person returned by TMDb.
/// </summary>
public sealed class PersonDetailsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("original_name")]
    public string OriginalName { get; init; } = string.Empty;

    [JsonPropertyName("also_known_as")] 
    public IReadOnlyList<string> AlsoKnownAs { get; init; } = [];

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }

    [JsonPropertyName("biography")]
    public string Biography { get; init; } = string.Empty;

    [JsonPropertyName("birthday")]
    public DateOnly? Birthday { get; init; }

    [JsonPropertyName("deathday")]
    public DateOnly? Deathday { get; init; }

    [JsonPropertyName("gender")]
    public int? Gender { get; init; }

    [JsonPropertyName("homepage")]
    public string? Homepage { get; init; }

    [JsonPropertyName("imdb_id")]
    public string? ImdbId { get; init; }

    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; init; }

    [JsonPropertyName("place_of_birth")]
    public string? PlaceOfBirth { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; init; }
}