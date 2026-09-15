using System.Text.Json.Serialization;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;
using WatchBook.Infrastructure.External.TMDb.Responses.Common;
namespace WatchBook.Infrastructure.External.TMDb.Responses.Movies;

/// <summary>
/// Represents the response returned by the TMDb movie details endpoint.
/// </summary>
public sealed class MovieDetailsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("title")]
    public string Title { get; init; } = string.Empty;

    [JsonPropertyName("original_title")]
    public string OriginalTitle { get; init; } = string.Empty;

    [JsonPropertyName("overview")]
    public string? Overview { get; init; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; init; }

    [JsonPropertyName("release_date")]
    public DateOnly? ReleaseDate { get; init; }

    [JsonPropertyName("runtime")]
    public int? Runtime { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("status")]
    public string? Status { get; init; }

    [JsonPropertyName("genres")]
    public IReadOnlyList<GenreResponse> Genres { get; init; } = [];

    [JsonPropertyName("production_companies")]
    public IReadOnlyList<CompanyResponse> ProductionCompanies { get; init; } = [];

    [JsonPropertyName("production_countries")]
    public IReadOnlyList<CountryResponse> ProductionCountries { get; init; } = [];
}