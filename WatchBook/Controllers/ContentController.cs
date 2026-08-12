using Microsoft.AspNetCore.Mvc;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Web.Controllers;

[ApiController]
[Route("api/content")]
public sealed class ContentController(
    IContentImportService contentImportService) : ControllerBase
{
    private readonly IContentImportService _contentImportService =
        contentImportService;

    /// <summary>
    /// Imports a movie from TMDb and saves it to the database.
    /// </summary>
    /// <param name="tmdbId">The TMDb movie identifier.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("import/movie/{tmdbId:int}")]
    public async Task<IActionResult> ImportMovie(
        int tmdbId,
        CancellationToken cancellationToken)
    {
        var content = await _contentImportService.ImportMovieAsync(
            tmdbId,
            cancellationToken);

        return Ok(new
        {
            success = true,
            message = "Movie imported and saved successfully.",
            contentId = content.Id,
            tmdbId = content.TmdbId,
            title = content.Title
        });
    }
}