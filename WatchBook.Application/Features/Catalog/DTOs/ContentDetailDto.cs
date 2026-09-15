using WatchBook.Domain.Features.Catalog.Enums;

namespace WatchBook.Application.Features.Catalog.DTOs;

public sealed class ContentDetailDto
{
    public int Id { get; init; }

    public int TmdbId { get; init; }

    public ContentType Type { get; init; }

    public string Title { get; init; } = string.Empty;

    public string? OriginalTitle { get; init; }

    public string? Overview { get; init; }

    public string? PosterPath { get; init; }

    public string? BackdropPath { get; init; }

    public DateOnly? ReleaseDate { get; init; }

    public int? Runtime { get; init; }

    public double Popularity { get; init; }

    public double VoteAverage { get; init; }

    public int VoteCount { get; init; }

    public ContentStatus Status { get; init; }

    public ProductionStatus ProductionStatus { get; init; }

    public IReadOnlyList<GenreDto> Genres { get; init; } = [];

    public IReadOnlyList<CompanyDto> Companies { get; init; } = [];

    public IReadOnlyList<CountryDto> Countries { get; init; } = [];

    public IReadOnlyList<PersonDto> People { get; init; } = [];

    public IReadOnlyList<SeasonDto> Seasons { get; init; } = [];
}