using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Features.UserContent.Services;
using WatchBook.Infrastructure.Features.System.Interfaces;

namespace WatchBook.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/watch-history")]
public sealed class WatchHistoryController(
    IWatchHistoryService watchHistoryService,
    ICurrentUserService currentUserService) : ControllerBase
{
    [HttpPost("{contentId:int}")]
    public async Task<IActionResult> Add(
        int contentId,
        [FromBody] AddWatchHistoryRequest? request,
        CancellationToken cancellationToken)
    {
        if (contentId <= 0)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid content ID",
                detail: "Content ID must be greater than zero.");
        }

        if (!currentUserService.IsAuthenticated
            || string.IsNullOrWhiteSpace(currentUserService.UserId))
        {
            return Unauthorized();
        }

        await watchHistoryService.AddAsync(
            currentUserService.UserId,
            contentId,
            request?.EpisodeId,
            request?.Rating,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Content added to watch history successfully.",
            EpisodeId = request?.EpisodeId,
            Rating = request?.Rating
        });
    }

    [HttpGet]
    public async Task<IActionResult> Get(
        CancellationToken cancellationToken)
    {
        if (!currentUserService.IsAuthenticated
            || string.IsNullOrWhiteSpace(currentUserService.UserId))
        {
            return Unauthorized();
        }

        var history = await watchHistoryService.GetAsync(
            currentUserService.UserId,
            cancellationToken);

        return Ok(history);
    }

    [HttpDelete("{historyId:int}")]
    public async Task<IActionResult> Remove(
        int historyId,
        CancellationToken cancellationToken)
    {
        if (historyId <= 0)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid history ID",
                detail: "History ID must be greater than zero.");
        }

        if (!currentUserService.IsAuthenticated
            || string.IsNullOrWhiteSpace(currentUserService.UserId))
        {
            return Unauthorized();
        }

        await watchHistoryService.RemoveAsync(
            currentUserService.UserId,
            historyId,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Watch history entry removed successfully."
        });
    }

    public sealed record AddWatchHistoryRequest(
        int? EpisodeId,
        decimal? Rating);
}