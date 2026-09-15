using WatchBook.Domain.Features.Catalog.Enums;

namespace WatchBook.Application.DTOs;

public sealed class PersonDto
{
    public int Id { get; init; }

    public int TmdbId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? OriginalName { get; init; }

    public string? ProfilePath { get; init; }

    public PersonRole Role { get; init; }

    public string? CharacterName { get; init; }

    public int DisplayOrder { get; init; }
}