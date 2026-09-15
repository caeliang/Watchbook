namespace WatchBook.Infrastructure.Features.System.Interfaces;

/// <summary>
/// Generates SEO-friendly URL slugs.
/// </summary>
public interface ISlugGenerator
{
    string Generate(string? value);
}