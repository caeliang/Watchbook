using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents movie credits returned by TMDb.
/// </summary>
public sealed class MovieCreditsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("cast")]
    public IReadOnlyList<MovieCastResponse> Cast { get; init; } = [];

    [JsonPropertyName("crew")]
    public IReadOnlyList<MovieCrewResponse> Crew { get; init; } = [];
}