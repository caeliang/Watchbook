using System.Net.Http.Headers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using WatchBook.Infrastructure.External.TMDb.Handlers;
using WatchBook.Infrastructure.External.TMDb.Options;

namespace WatchBook.Infrastructure.External.TMDb.Extensions;

/// <summary>
/// Provides extension methods for configuring TMDb HTTP clients.
/// </summary>
public static class HttpClientBuilderExtensions
{
    public static IHttpClientBuilder AddTmdbClient<TClient, TImplementation>(
        this IServiceCollection services)
        where TClient : class
        where TImplementation : class, TClient
    {
        return services
            .AddHttpClient<TClient, TImplementation>((provider, client) =>
            {
                var options = provider
                    .GetRequiredService<IOptions<TmdbOptions>>()
                    .Value;

                client.BaseAddress = new Uri(options.BaseUrl);

                client.Timeout = options.Timeout;

                client.DefaultRequestHeaders.Accept.Clear();

                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
            })
            .AddHttpMessageHandler<TmdbAuthenticationHandler>();
    }
}
