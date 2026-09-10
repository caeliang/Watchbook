namespace WatchBook.Application.DTOs;

public sealed class GenreDto
{
    public int Id { get; init; }

    public int TmdbId { get; init; }

    public string Name { get; init; } = string.Empty;
}