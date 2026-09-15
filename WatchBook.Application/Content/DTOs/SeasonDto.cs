namespace WatchBook.Application.DTOs;

public sealed class SeasonDto
{
    public int Id { get; init; }

    public int TmdbId { get; init; }

    public int SeasonNumber { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Overview { get; init; }

    public string? PosterPath { get; init; }

    public DateOnly? AirDate { get; init; }

    public int EpisodeCount { get; init; }

    public IReadOnlyList<EpisodeDto> Episodes { get; init; } = [];
}