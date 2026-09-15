using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Features.UserContent.Services;
using WatchBook.Domain.Features.UserContent.Enums;
using WatchBook.Infrastructure.Features.System.Interfaces;

namespace WatchBook.Web.Features.UserContent.Controllers;

[ApiController]
[Authorize]
[Route("api/watch-status")]
public sealed class WatchStatusController(
    IWatchStatusService watchStatusService,
    ICurrentUserService currentUserService) : ControllerBase
{
    [HttpPut("{contentId:int}")]
    public async Task<IActionResult> Set(
        int contentId,
        [FromBody] SetWatchStatusRequest request,
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

        if (!Enum.IsDefined(request.Status))
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid watch status",
                detail: "Watch status must be Watching, Completed, or Dropped.");
        }

        await watchStatusService.SetAsync(
            currentUserService.UserId,
            contentId,
            request.Status,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Watch status updated successfully.",
            Status = request.Status.ToString()
        });
    }

    [HttpGet("{contentId:int}")]
    public async Task<IActionResult> Get(
        int contentId,
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

        var status = await watchStatusService.GetAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            HasStatus = status.HasValue,
            Status = status?.ToString()
        });
    }

    [HttpDelete("{contentId:int}")]
    public async Task<IActionResult> Remove(
        int contentId,
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

        await watchStatusService.RemoveAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Watch status removed successfully."
        });
    }

    public sealed record SetWatchStatusRequest(
        WatchStatusType Status);
}