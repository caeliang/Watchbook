namespace WatchBook.Infrastructure.Services.Interfaces;

/// <summary>
/// Generates SEO-friendly URL slugs.
/// </summary>
public interface ISlugGenerator
{
    string Generate(string? value);
}