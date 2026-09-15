using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Features.UserContent.Services;
using WatchBook.Infrastructure.Features.System.Interfaces;

namespace WatchBook.Web.Features.UserContent.Controllers;

[ApiController]
[Authorize]
[Route("api/favorites")]
public sealed class FavoriteController(
    IFavoriteService favoriteService,
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

        await favoriteService.AddAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Content added to favorites successfully."
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

        await favoriteService.RemoveAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            Success = true,
            Message = "Content removed from favorites successfully."
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

        var exists = await favoriteService.ExistsAsync(
            currentUserService.UserId,
            contentId,
            cancellationToken);

        return Ok(new
        {
            IsFavorite = exists
        });
    }
}