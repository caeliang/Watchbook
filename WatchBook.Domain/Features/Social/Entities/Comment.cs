using System.ComponentModel.DataAnnotations;

using WatchBook.Domain.Common;
using WatchBook.Domain.Features.Catalog.Entities;

namespace WatchBook.Domain.Features.Social.Entities;

/// <summary>
/// Represents a user's comment on a movie or TV series.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    /// The identifier of the user who created the comment.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// The identifier of the content being commented on.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The comment text.
    /// </summary>
    [Required]
    [MaxLength(1000)]
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The content associated with this comment.
    /// </summary>
    public Content Content { get; set; } = null!;
}
