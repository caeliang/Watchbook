using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using WatchBook.Application.Features.Social.Interfaces;

namespace WatchBook.Features.Social.Controllers;

/// <summary>
/// Handles user comment operations.
/// </summary>
[Route("comments")]
public class CommentController : Controller
{
    private readonly ICommentService _commentService;

    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    /// <summary>
    /// Gets all active comments for the specified content.
    /// </summary>
    [HttpGet("content/{contentId:int}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByContent(
        int contentId,
        CancellationToken cancellationToken)
    {
        var comments = await _commentService.GetByContentAsync(
            contentId,
            cancellationToken);

        return Ok(comments);
    }

    /// <summary>
    /// Creates a new comment for the specified content.
    /// </summary>
    [Authorize]
    [HttpPost("create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
        int contentId,
        string text,
        CancellationToken cancellationToken)
    {
        var commentId = await _commentService.CreateAsync(
            contentId,
            text,
            cancellationToken);

        return Ok(new
        {
            commentId
        });
    }

    /// <summary>
    /// Updates an existing comment.
    /// </summary>
    [Authorize]
    [HttpPost("update")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(
        int commentId,
        string text,
        CancellationToken cancellationToken)
    {
        await _commentService.UpdateAsync(
            commentId,
            text,
            cancellationToken);

        return Ok();
    }

    /// <summary>
    /// Soft-deletes an existing comment.
    /// </summary>
    [Authorize]
    [HttpPost("delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(
        int commentId,
        CancellationToken cancellationToken)
    {
        await _commentService.DeleteAsync(
            commentId,
            cancellationToken);

        return Ok();
    }
}