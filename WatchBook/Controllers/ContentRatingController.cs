using Microsoft.AspNetCore.Mvc;
using WatchBook.Application.Features.UserContent.Services;

namespace WatchBook.Web.Controllers;

[ApiController]
[Route("api/content")]
public sealed class ContentRatingController(
    IContentRatingService contentRatingService) : ControllerBase
{
    [HttpGet("{contentId:int}/rating")]
    public async Task<IActionResult> GetRating(
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

        var rating = await contentRatingService.GetAsync(
            contentId,
            cancellationToken);

        if (rating is null)
        {
            return NotFound(new
            {
                Success = false,
                Message = "Content was not found."
            });
        }

        return Ok(rating);
    }
}