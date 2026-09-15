using WatchBook.Domain.Common;
using WatchBook.Domain.Features.Catalog.Entities;
using WatchBook.Domain.Features.UserContent.Enums;
namespace WatchBook.Domain.Features.UserContent.Entities;

public class WatchStatus : BaseEntity
{
    public string UserId { get; set; } = string.Empty;

    public int ContentId { get; set; }

    public WatchStatusType Status { get; set; }

    public Content Content { get; set; }
}