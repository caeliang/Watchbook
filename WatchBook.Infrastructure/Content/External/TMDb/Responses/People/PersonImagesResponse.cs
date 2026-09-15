using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.People;

/// <summary>
/// Represents person images returned by TMDb.
/// </summary>
public sealed class PersonImagesResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("profiles")]
    public IReadOnlyList<PersonImageResponse> Profiles { get; init; } = [];
}