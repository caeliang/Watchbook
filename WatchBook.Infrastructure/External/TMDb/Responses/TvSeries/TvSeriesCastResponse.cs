using System.Text.Json.Serialization;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents a cast member of a TV series.
/// </summary>
public sealed class TvSeriesCastResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("adult")]
    public bool Adult { get; init; }

    [JsonPropertyName("gender")]
    public int? Gender { get; init; }

    [JsonPropertyName("known_for_department")]
    public string? KnownForDepartment { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("original_name")]
    public string OriginalName { get; init; } = string.Empty;

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("profile_path")]
    public string? ProfilePath { get; init; }

    [JsonPropertyName("character")]
    public string Character { get; init; } = string.Empty;

    [JsonPropertyName("credit_id")]
    public string CreditId { get; init; } = string.Empty;

    [JsonPropertyName("order")]
    public int Order { get; init; }
}