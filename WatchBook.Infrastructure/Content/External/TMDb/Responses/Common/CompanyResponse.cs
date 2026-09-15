using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.Common;

/// <summary>
/// Represents a production company returned by TMDb.
/// </summary>
public sealed class CompanyResponse
{
    /// <summary>
    /// Gets the TMDb company identifier.
    /// </summary>
    [JsonPropertyName("id")]
    public int Id { get; init; }

    /// <summary>
    /// Gets the company name.
    /// </summary>
    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    /// <summary>
    /// Gets the company logo path.
    /// </summary>
    [JsonPropertyName("logo_path")]
    public string? LogoPath { get; init; }

    /// <summary>
    /// Gets the ISO country code.
    /// </summary>
    [JsonPropertyName("origin_country")]
    public string? OriginCountry { get; init; }
}