namespace WatchBook.Infrastructure.External.TMDb.Options;

public sealed class TmdbOptions
{
    public const string SectionName = "TMDb";

    public string AccessToken { get; init; } = string.Empty;

    public string BaseUrl { get; init; } = string.Empty;

    public string ImageBaseUrl { get; init; } = string.Empty;

    public TimeSpan Timeout { get; init; } = TimeSpan.FromSeconds(30);
}