using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.Movies;
using WatchBook.Infrastructure.External.TMDb.Responses.People;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

/// <summary>
/// Synchronizes person entities from TMDb responses.
/// Handles both full person details (from dedicated person endpoints) and
/// abbreviated person info (from cast/crew credits).
/// Implements idempotent sync pattern: looks up person by TmdbId,
/// creates if not found, updates properties, and persists to database.
/// </summary>
public sealed class PersonSyncService
{
    private readonly WatchBookDbContext _dbContext;

    public PersonSyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    /// <summary>
    /// Synchronizes a person entity from detailed TMDb response.
    /// Updates available person properties including biography, birthday, etc.
    /// </summary>
    /// <param name="response">The detailed TMDb person response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The synchronized person entity.</returns>
    public async Task<Person> SyncAsync(
        PersonDetailsResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var person = await _dbContext.People
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (person is null)
        {
            person = new Person
            {
                TmdbId = response.Id
            };

            _dbContext.People.Add(person);
        }

        person.Name = response.Name;
        person.OriginalName = response.OriginalName;
        person.Biography = response.Biography;
        person.ProfilePath = response.ProfilePath;
        person.Birthday = response.Birthday;
        person.Deathday = response.Deathday;
        person.PlaceOfBirth = response.PlaceOfBirth;
        person.IsAdult = response.Adult;
        person.Popularity = response.Popularity;
        // Note: PersonDetailsResponse also has Gender, Homepage, ImdbId, KnownForDepartment,
        // but the Person entity does not currently store these fields

        await _dbContext.SaveChangesAsync(cancellationToken);

        return person;
    }

    /// <summary>
    /// Synchronizes a person entity from cast member response (abbreviated info).
    /// Updates only the basic properties available in cast data.
    /// </summary>
    /// <param name="response">The TMDb cast response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The synchronized person entity.</returns>
    public async Task<Person> SyncAsync(
        MovieCastResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var person = await _dbContext.People
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (person is null)
        {
            person = new Person
            {
                TmdbId = response.Id
            };

            _dbContext.People.Add(person);
        }

        person.Name = response.Name;
        person.OriginalName = response.OriginalName;
        person.ProfilePath = response.ProfilePath;
        // Note: Cast responses don't include Gender, Adult, Popularity fields
        // Only update what's available

        await _dbContext.SaveChangesAsync(cancellationToken);

        return person;
    }

    /// <summary>
    /// Synchronizes a person entity from crew member response (abbreviated info).
    /// Updates only the basic properties available in crew data.
    /// </summary>
    /// <param name="response">The TMDb crew response.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The synchronized person entity.</returns>
    public async Task<Person> SyncAsync(
        MovieCrewResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);

        var person = await _dbContext.People
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);

        if (person is null)
        {
            person = new Person
            {
                TmdbId = response.Id
            };

            _dbContext.People.Add(person);
        }

        person.Name = response.Name;
        person.OriginalName = response.OriginalName;
        person.ProfilePath = response.ProfilePath;
        // Note: Crew responses don't include Gender, Adult, Popularity fields
        // Only update what's available

        await _dbContext.SaveChangesAsync(cancellationToken);

        return person;
    }
}
