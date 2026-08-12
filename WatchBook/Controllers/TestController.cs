using Microsoft.AspNetCore.Mvc;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Web.Controllers;

[ApiController]
[Route("api/test")]
public class TestController : ControllerBase
{
    private readonly IContentImportService _contentImportService;

    public TestController(
        IContentImportService contentImportService)
    {
        _contentImportService = contentImportService;
    }

    [HttpGet("import-movie/{tmdbId:int}")]
    public async Task<IActionResult> ImportMovie(
        int tmdbId,
        CancellationToken cancellationToken)
    {
        var result = await _contentImportService.ImportMovieAsync(
            tmdbId,
            cancellationToken);

        return Ok(new
        {
            success = true,
            message = "Movie imported and saved successfully.",
            contentId = result.Id,
            tmdbId = result.TmdbId,
            title = result.Title
        });
    }
}