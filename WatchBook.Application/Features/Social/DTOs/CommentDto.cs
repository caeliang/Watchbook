namespace WatchBook.Application.Features.Social.DTOs;

/// <summary>
/// Represents a comment returned to the application layer.
/// </summary>
public class CommentDto
{
    /// <summary>
    /// The unique identifier of the comment.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// The identifier of the user who created the comment.
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    /// The comment text.
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// The date and time when the comment was created.
    /// </summary>
    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// The date and time when the comment was last updated.
    /// </summary>
    public DateTime UpdatedAt { get; set; }
}