namespace WatchBook.Web.Models.Content;

public sealed class ContentImportResponse
{
    public bool Success { get; init; }

    public string Message { get; init; } = string.Empty;

    public int ContentId { get; init; }

    public int TmdbId { get; init; }

    public string Title { get; init; } = string.Empty;
}