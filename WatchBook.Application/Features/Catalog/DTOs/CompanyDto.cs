namespace WatchBook.Application.Features.Catalog.DTOs;

public sealed class CompanyDto
{
    public int Id { get; init; }

    public int TmdbId { get; init; }

    public string Name { get; init; } = string.Empty;

    public string? Homepage { get; init; }

    public string? LogoPath { get; init; }

    public string? OriginCountry { get; init; }
}