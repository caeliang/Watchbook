using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Features.Social.Interfaces;

namespace WatchBook.Controllers;

[Route("api/test")]
public class TestController : Controller
{
    private readonly ICommentService _commentService;

    public TestController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    // GET: /api/test/comments/{contentId}
    [HttpGet("comments/{contentId:int}")]
    public async Task<IActionResult> GetComments(
        int contentId,
        CancellationToken cancellationToken)
    {
        var comments = await _commentService.GetByContentAsync(
            contentId,
            cancellationToken);

        return Ok(comments);
    }

    // POST: /api/test/comments/{contentId}
    [HttpPost("comments/{contentId:int}")]
    public async Task<IActionResult> CreateComment(
        int contentId,
        [FromBody] CreateCommentRequest request,
        CancellationToken cancellationToken)
    {
        var commentId = await _commentService.CreateAsync(
            contentId,
            request.Text,
            cancellationToken);

        return Ok(new
        {
            commentId,
            contentId,
            request.Text
        });
    }

    // PUT: /api/test/comments/{commentId}
    [HttpPut("comments/{commentId:int}")]
    public async Task<IActionResult> UpdateComment(
        int commentId,
        [FromBody] UpdateCommentRequest request,
        CancellationToken cancellationToken)
    {
        await _commentService.UpdateAsync(
            commentId,
            request.Text,
            cancellationToken);

        return Ok(new
        {
            commentId,
            request.Text
        });
    }

    // DELETE: /api/test/comments/{commentId}
    [HttpDelete("comments/{commentId:int}")]
    public async Task<IActionResult> DeleteComment(
        int commentId,
        CancellationToken cancellationToken)
    {
        await _commentService.DeleteAsync(
            commentId,
            cancellationToken);

        return Ok(new
        {
            commentId,
            deleted = true
        });
    }
}

public sealed class CreateCommentRequest
{
    public string Text { get; set; } = string.Empty;
}

public sealed class UpdateCommentRequest
{
    public string Text { get; set; } = string.Empty;
}