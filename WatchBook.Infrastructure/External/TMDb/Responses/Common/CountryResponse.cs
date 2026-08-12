using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Common;

/// <summary>
/// Represents a production country returned by TMDb.
/// </summary>
public sealed class CountryResponse
{
    /// <summary>
    /// Gets the ISO 3166-1 country code.
    /// </summary>
    [JsonPropertyName("iso_3166_1")]
    public string Code { get; init; } = string.Empty;

    /// <summary>
    /// Gets the country name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;
}