using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Interfaces;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Web.Controllers;

[ApiController]
[Authorize]
[Route("api/watchlist")]
public sealed class WatchlistController(
    IWatchlistService watchlistService,
    ICurrentUserService currentUserService) : ControllerBase
{
    [HttpPost("{contentId:int}")]
    public async Task<IActionResult> Add(
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

        await watchlistService.AddAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Content added to watchlist successfully."
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

        await watchlistService.RemoveAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Content removed from watchlist successfully."
        });
    }

    [HttpGet("{contentId:int}")]
    public async Task<IActionResult> Exists(
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

        var exists = await watchlistService.ExistsAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            InWatchlist = exists
        });
    }
}
