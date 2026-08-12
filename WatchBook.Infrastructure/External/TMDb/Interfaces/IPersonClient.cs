using WatchBook.Infrastructure.External.TMDb.Responses.People;

namespace WatchBook.Infrastructure.External.TMDb.Interfaces;

/// <summary>
/// Provides access to TMDb person endpoints.
/// </summary>
public interface IPersonClient
{
    Task<PersonDetailsResponse> GetDetailsAsync(
        int personId,
        CancellationToken cancellationToken = default);

    Task<PersonCombinedCreditsResponse> GetCombinedCreditsAsync(
        int personId,
        CancellationToken cancellationToken = default);

    Task<PersonImagesResponse> GetImagesAsync(
        int personId,
        CancellationToken cancellationToken = default);

    Task<PersonExternalIdsResponse> GetExternalIdsAsync(
        int personId,
        CancellationToken cancellationToken = default);

    Task<PersonListResponse> GetPopularAsync(
        int page = 1,
        CancellationToken cancellationToken = default);
}