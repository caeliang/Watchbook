using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using WatchBook.Infrastructure.External.TMDb.Options;

namespace WatchBook.Infrastructure.External.TMDb.Handlers;


/// <summary>
/// Automatically appends the TMDb Bearer authentication token
/// to every outgoing HTTP request.
/// </summary>
public sealed class TmdbAuthenticationHandler(
    IOptions<TmdbOptions> options)
    : DelegatingHandler
{
    private readonly string _token = options.Value.AccessToken;


    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);


        if (!string.IsNullOrWhiteSpace(_token))
        {
            request.Headers.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    _token);
        }


        return base.SendAsync(
            request,
            cancellationToken);
    }
}