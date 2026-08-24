using Microsoft.AspNetCore.Mvc;
using WatchBook.Infrastructure.Services.Interfaces;
using WatchBook.Web.Models.Content;

namespace WatchBook.Controllers;

[ApiController]
[Route("api/content")]
public sealed class ContentController(
    IContentImportService contentImportService) : ControllerBase
{
    [HttpPost("import/movie/{tmdbId:int}")]
    public async Task<IActionResult> ImportMovie(
        int tmdbId,
        CancellationToken cancellationToken)
    {
        if (tmdbId <= 0)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid TMDb ID",
                detail: "TMDb ID must be greater than zero.");
        }

        var content = await contentImportService.ImportMovieAsync(
            tmdbId,
            cancellationToken);

        return Ok(new ContentImportResponse
        {
            Success = true,
            Message = "Movie imported and saved successfully.",
            ContentId = content.Id,
            TmdbId = content.TmdbId,
            Title = content.Title
        });
    }

    [HttpPost("import/tv/{tmdbId:int}")]
    public async Task<IActionResult> ImportTvSeries(
        int tmdbId,
        CancellationToken cancellationToken)
    {
        if (tmdbId <= 0)
        {
            return Problem(
                statusCode: StatusCodes.Status400BadRequest,
                title: "Invalid TMDb ID",
                detail: "TMDb ID must be greater than zero.");
        }

        var content = await contentImportService.ImportTvSeriesAsync(
            tmdbId,
            cancellationToken);

        return Ok(new ContentImportResponse
        {
            Success = true,
            Message = "TV series imported and saved successfully.",
            ContentId = content.Id,
            TmdbId = content.TmdbId,
            Title = content.Title
        });
    }
}