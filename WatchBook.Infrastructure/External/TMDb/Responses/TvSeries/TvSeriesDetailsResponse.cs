using System.Text.Json.Serialization;
using WatchBook.Infrastructure.External.TMDb.Responses.Common;

namespace WatchBook.Infrastructure.External.TMDb.Responses.TvSeries;

/// <summary>
/// Represents detailed information about a TV series returned by TMDb.
/// </summary>
public sealed class TvSeriesDetailsResponse
{
    [JsonPropertyName("id")]
    public int Id { get; init; }

    [JsonPropertyName("name")]
    public string Name { get; init; } = string.Empty;

    [JsonPropertyName("original_name")]
    public string OriginalName { get; init; } = string.Empty;

    [JsonPropertyName("overview")]
    public string Overview { get; init; } = string.Empty;

    [JsonPropertyName("tagline")]
    public string? Tagline { get; init; }

    [JsonPropertyName("homepage")]
    public string? Homepage { get; init; }

    [JsonPropertyName("status")]
    public string Status { get; init; } = string.Empty;

    [JsonPropertyName("type")]
    public string Type { get; init; } = string.Empty;

    [JsonPropertyName("original_language")]
    public string OriginalLanguage { get; init; } = string.Empty;

    [JsonPropertyName("first_air_date")]
    public string? FirstAirDate { get; init; }

    [JsonPropertyName("last_air_date")]
    public string? LastAirDate { get; init; }

    [JsonPropertyName("number_of_seasons")]
    public int NumberOfSeasons { get; init; }

    [JsonPropertyName("number_of_episodes")]
    public int NumberOfEpisodes { get; init; }

    [JsonPropertyName("episode_run_time")]
    public IReadOnlyList<int> EpisodeRunTime { get; init; } = [];

    [JsonPropertyName("in_production")]
    public bool InProduction { get; init; }

    [JsonPropertyName("popularity")]
    public double Popularity { get; init; }

    [JsonPropertyName("vote_average")]
    public double VoteAverage { get; init; }

    [JsonPropertyName("vote_count")]
    public int VoteCount { get; init; }

    [JsonPropertyName("poster_path")]
    public string? PosterPath { get; init; }

    [JsonPropertyName("backdrop_path")]
    public string? BackdropPath { get; init; }

    [JsonPropertyName("genres")]
    public IReadOnlyList<GenreResponse> Genres { get; init; } = [];

    [JsonPropertyName("networks")]
    public IReadOnlyList<NetworkResponse> Networks { get; init; } = [];

    [JsonPropertyName("production_companies")]
    public IReadOnlyList<CompanyResponse> ProductionCompanies { get; init; } = [];

    [JsonPropertyName("origin_country")]
    public IReadOnlyList<string> OriginCountries { get; init; } = [];

    [JsonPropertyName("production_countries")]
    public IReadOnlyList<CountryResponse> ProductionCountries { get; init; } = [];

    [JsonPropertyName("spoken_languages")]
    public IReadOnlyList<LanguageResponse> SpokenLanguages { get; init; } = [];
}