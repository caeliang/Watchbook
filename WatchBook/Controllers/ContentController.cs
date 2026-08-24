using Microsoft.AspNetCore.Mvc;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Web.Controllers;

[ApiController]
[Route("api/content")]
public sealed class ContentController(
    IContentImportService contentImportService,
    ITvSeriesClient tvSeriesClient) : ControllerBase
{
    private readonly IContentImportService _contentImportService =
        contentImportService;

    private readonly ITvSeriesClient _tvSeriesClient =
        tvSeriesClient;

    /// <summary>
    /// Imports a TV series from TMDb and saves it to the database.
    /// </summary>
    /// <param name="tmdbId">The TMDb TV series identifier.</param>
    /// <param name="cancellationToken">Request cancellation token.</param>
    [HttpPost("import/tv/{tmdbId:int}")]
    public async Task<IActionResult> ImportTvSeries(
        int tmdbId,
        CancellationToken cancellationToken)
    {
        var response = await _tvSeriesClient.GetDetailsAsync(
            tmdbId,
            cancellationToken);

        var content = await _contentImportService.ImportTvSeriesAsync(
            response,
            cancellationToken);

        return Ok(new
        {
            success = true,
            message = "TV series imported and saved successfully.",
            contentId = content.Id,
            tmdbId = content.TmdbId,
            title = content.Title
        });
    }
}