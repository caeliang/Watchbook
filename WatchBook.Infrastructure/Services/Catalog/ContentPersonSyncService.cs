using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Domain.Enums.Content;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

/// <summary>
/// Synchronizes content-person relationships (cast and crew) from TMDb credits responses.
/// Handles the complete relationship sync: clears existing relations and rebuilds them
/// from the credits response, ensuring idempotent behavior.
/// </summary>
public sealed class ContentPersonSyncService
{
    private readonly WatchBookDbContext _dbContext;
    private readonly PersonSyncService _personSyncService;

    public ContentPersonSyncService(
        WatchBookDbContext dbContext,
        PersonSyncService personSyncService)
    {
        _dbContext = dbContext;
        _personSyncService = personSyncService;
    }

    /// <summary>
    /// Synchronizes all cast and crew members for a piece of content from TMDb credits.
    /// Removes all existing content-person relations and recreates them from the credits response.
    /// This ensures the database reflects the latest TMDb data.
    /// </summary>
    /// <param name="content">The content entity to sync relations for.</param>
    /// <param name="credits">The TMDb credits response containing cast and crew.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    public async Task SyncAsync(
        Content content,
        MovieCreditsResponse credits,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(content);
        ArgumentNullException.ThrowIfNull(credits);

        // Remove all existing relations for this content
        var existingRelations = await _dbContext.ContentPeople
            .Where(x => x.ContentId == content.Id)
            .ToListAsync(cancellationToken);

        if (existingRelations.Count > 0)
        {
            _dbContext.ContentPeople.RemoveRange(existingRelations);
        }

        // Sync cast members
        int displayOrder = 0;
        foreach (var castResponse in credits.Cast)
        {
            var person = await _personSyncService.SyncAsync(
                castResponse,
                cancellationToken);

            _dbContext.ContentPeople.Add(new ContentPerson
            {
                ContentId = content.Id,
                PersonId = person.Id,
                Role = PersonRole.Actor,
                CharacterName = castResponse.Character,
                DisplayOrder = displayOrder++
            });
        }

        // Sync crew members (directors, writers, producers, etc.)
        // Note: For crew, we only sync roles we recognize; generic crew is optional
        foreach (var crewResponse in credits.Crew)
        {
            var personRole = DeterminePersonRole(crewResponse.Job);
            if (personRole is null)
            {
                continue; // Skip crew with unrecognized roles
            }

            var person = await _personSyncService.SyncAsync(
                crewResponse,
                cancellationToken);

            _dbContext.ContentPeople.Add(new ContentPerson
            {
                ContentId = content.Id,
                PersonId = person.Id,
                Role = personRole.Value,
                CharacterName = null,
                DisplayOrder = 0
            });
        }

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Maps TMDb crew job titles to PersonRole enum values.
    /// Returns null for unrecognized crew roles.
    /// </summary>
    private static PersonRole? DeterminePersonRole(string? job)
    {
        return job?.ToLowerInvariant() switch
        {
            "director" => PersonRole.Director,
            "writer" => PersonRole.Writer,
            "producer" => PersonRole.Producer,
            _ => null
        };
    }
}