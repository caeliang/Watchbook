using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Domain.Entities.User;

public class WatchHistory : BaseEntity
{
    public string UserId { get; set; } = string.Empty;

    public int ContentId { get; set; }

    public int? EpisodeId { get; set; }

    public DateTime WatchedAt { get; set; } = DateTime.UtcNow;

    public decimal? Rating { get; set; }

    public Content Content { get; set; } = null!;

    public Episode? Episode { get; set; }
}