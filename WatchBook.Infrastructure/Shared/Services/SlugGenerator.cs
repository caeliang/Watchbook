using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Infrastructure.Services;

/// <summary>
/// Generates SEO-friendly slugs.
/// </summary>
public sealed class SlugGenerator : ISlugGenerator
{
    private static readonly Regex NonAlphaNumericRegex =
        new("[^a-z0-9]+", RegexOptions.Compiled);

    private static readonly Regex MultiDashRegex =
        new("-{2,}", RegexOptions.Compiled);

    public string Generate(string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return string.Empty;

        value = value.Trim().ToLowerInvariant();

        value = RemoveDiacritics(value);

        value = value
            .Replace('ı', 'i')
            .Replace('ğ', 'g')
            .Replace('ü', 'u')
            .Replace('ş', 's')
            .Replace('ö', 'o')
            .Replace('ç', 'c');

        value = NonAlphaNumericRegex.Replace(value, "-");

        value = MultiDashRegex.Replace(value, "-");

        return value.Trim('-');
    }

    private static string RemoveDiacritics(string text)
    {
        var normalized = text.Normalize(NormalizationForm.FormD);

        var builder = new StringBuilder();

        foreach (var c in normalized)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        return builder
            .ToString()
            .Normalize(NormalizationForm.FormC);
    }
}