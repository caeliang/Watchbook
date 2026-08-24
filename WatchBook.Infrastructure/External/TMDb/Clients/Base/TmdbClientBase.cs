using System.Net.Http.Json;
using System.Text.Json;

namespace WatchBook.Infrastructure.External.TMDb.Clients.Base;

/// <summary>
/// Provides common HTTP functionality for TMDb clients.
/// </summary>
public abstract class TmdbClientBase(HttpClient httpClient)
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };


    protected async Task<T> GetAsync<T>(
        string requestUri,
        CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"TMDb Request Started: {requestUri}");

        using var response = await httpClient.GetAsync(
            requestUri,
            cancellationToken);


        Console.WriteLine(
            $"TMDb Response Received: {requestUri} - {(int)response.StatusCode}");


        if (response.IsSuccessStatusCode == false)
        {
            await HandleErrorAsync(
                response,
                requestUri,
                cancellationToken);
        }


        var result = await response.Content
            .ReadFromJsonAsync<T>(
                JsonOptions,
                cancellationToken);


        Console.WriteLine(
            $"TMDb Deserialize Completed: {requestUri}");


        return result
            ?? throw new InvalidOperationException(
                $"TMDb returned an empty response for '{requestUri}'.");
    }


    private static async Task HandleErrorAsync(
        HttpResponseMessage response,
        string requestUri,
        CancellationToken cancellationToken)
    {
        var message =
            await response.Content.ReadAsStringAsync(
                cancellationToken);


        throw new HttpRequestException(
            $"TMDb request failed. " +
            $"Status: {(int)response.StatusCode} " +
            $"({response.StatusCode}). " +
            $"Uri: {requestUri}. " +
            $"Response: {message}",
            null,
            response.StatusCode);
    }
}