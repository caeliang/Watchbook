using WatchBook.Domain.Common;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Enums;

namespace WatchBook.Domain.Entities.User;

public class WatchStatus : BaseEntity
{
    public string UserId { get; set; } = string.Empty;

    public int ContentId { get; set; }

    public WatchStatusType Status { get; set; }

    public Content Content { get; set; } = null!;
}