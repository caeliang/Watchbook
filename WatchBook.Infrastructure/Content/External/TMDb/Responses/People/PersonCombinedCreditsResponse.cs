using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.People;

/// <summary>
/// Represents all movie and TV credits for a person.
/// </summary>
public sealed class PersonCombinedCreditsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("cast")]
    public IReadOnlyList<PersonMovieCreditResponse> Cast { get; init; } = [];

    [JsonPropertyName("crew")]
    public IReadOnlyList<PersonMovieCreditResponse> Crew { get; init; } = [];
}