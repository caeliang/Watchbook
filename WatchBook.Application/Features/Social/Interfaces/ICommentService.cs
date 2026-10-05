using WatchBook.Application.Features.Social.DTOs;

namespace WatchBook.Application.Features.Social.Interfaces;

/// <summary>
/// Provides operations for managing user comments on content.
/// </summary>
public interface ICommentService
{
    /// <summary>
    /// Gets all active comments for the specified content.
    /// </summary>
    Task<IReadOnlyList<CommentDto>> GetByContentAsync(
        int contentId,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Creates a new comment for the current user.
    /// </summary>
    Task<int> CreateAsync(
        int contentId,
        string text,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Updates an existing comment owned by the current user.
    /// </summary>
    Task UpdateAsync(
        int commentId,
        string text,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Soft-deletes an existing comment owned by the current user.
    /// </summary>
    Task DeleteAsync(
        int commentId,
        CancellationToken cancellationToken = default);
}