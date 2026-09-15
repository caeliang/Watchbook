using WatchBook.Domain.Common;
using WatchBook.Domain.Features.Catalog.Entities;

namespace WatchBook.Domain.Features.UserContent.Entities;

public class Watchlist : BaseEntity
{
    public string UserId { get; set; } = string.Empty;

    public int ContentId { get; set; }

    public Content Content { get; set; } = null!;
}