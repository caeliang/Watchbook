using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Common;

/// <summary>
/// Represents a language returned by TMDb.
/// </summary>
public sealed class LanguageResponse
{
    [JsonPropertyName("iso_639_1")]
    public string Iso6391 { get; init; } = string.Empty;

    [JsonPropertyName("english_name")]
    public string EnglishName { get; init; } = string.Empty;

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}