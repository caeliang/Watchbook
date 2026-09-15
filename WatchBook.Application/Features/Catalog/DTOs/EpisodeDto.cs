namespace WatchBook.Application.Features.Catalog.DTOs;

public sealed class EpisodeDto
{
    public int Id { get; init; }

    public int TmdbId { get; init; }

    public int EpisodeNumber { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Overview { get; init; }

    public DateOnly? AirDate { get; init; }

    public int? Runtime { get; init; }

    public string? StillPath { get; init; }

    public double? VoteAverage { get; init; }

    public int? VoteCount { get; init; }
}