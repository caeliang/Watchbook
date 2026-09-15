using WatchBook.Infrastructure.External.TMDb.Clients.Base;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Responses.People;

namespace WatchBook.Infrastructure.External.TMDb.Clients;

public sealed class PersonClient(HttpClient httpClient)
    : TmdbClientBase(httpClient), IPersonClient
{
    public Task<PersonDetailsResponse> GetDetailsAsync(
        int personId,
        CancellationToken cancellationToken = default)
        => GetAsync<PersonDetailsResponse>(
            $"person/{personId}",
            cancellationToken);

    public Task<PersonCombinedCreditsResponse> GetCombinedCreditsAsync(
        int personId,
        CancellationToken cancellationToken = default)
        => GetAsync<PersonCombinedCreditsResponse>(
            $"person/{personId}/combined_credits",
            cancellationToken);

    public Task<PersonImagesResponse> GetImagesAsync(
        int personId,
        CancellationToken cancellationToken = default)
        => GetAsync<PersonImagesResponse>(
            $"person/{personId}/images",
            cancellationToken);

    public Task<PersonExternalIdsResponse> GetExternalIdsAsync(
        int personId,
        CancellationToken cancellationToken = default)
        => GetAsync<PersonExternalIdsResponse>(
            $"person/{personId}/external_ids",
            cancellationToken);

    public Task<PersonListResponse> GetPopularAsync(
        int page = 1,
        CancellationToken cancellationToken = default)
        => GetAsync<PersonListResponse>(
            $"person/popular?page={page}",
            cancellationToken);
}