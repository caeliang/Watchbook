using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchBook.Infrastructure.External.TMDb.Clients;
using WatchBook.Infrastructure.External.TMDb.Extensions;
using WatchBook.Infrastructure.External.TMDb.Handlers;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Options;
using WatchBook.Infrastructure.Identity;
using WatchBook.Infrastructure.Identity.Configurations;
using WatchBook.Infrastructure.Persistence;
using WatchBook.Infrastructure.Services;
using WatchBook.Infrastructure.Services.Interfaces;
using WatchBook.Infrastructure.Services.Catalog;
using WatchBook.Infrastructure.Services.Import;
namespace WatchBook.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(services);
        ArgumentNullException.ThrowIfNull(configuration);

        // Database
        services.AddDbContext<WatchBookDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        // Identity
        services
            .AddIdentity<ApplicationUser, ApplicationRole>(options =>
            {
                // Password
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 10;

                // User
                options.User.RequireUniqueEmail = true;

                // Sign In
                options.SignIn.RequireConfirmedAccount = false;
            })
            .AddEntityFrameworkStores<WatchBookDbContext>()
            .AddDefaultTokenProviders();

        services.AddIdentityCookieConfiguration();

        // TMDb
        services
            .AddOptions<TmdbOptions>()
            .Bind(configuration.GetSection(TmdbOptions.SectionName))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddTransient<TmdbAuthenticationHandler>();

        services.AddTmdbClient<IMovieClient, MovieClient>();
        services.AddTmdbClient<ITvSeriesClient, TvSeriesClient>();
        services.AddTmdbClient<ISearchClient, SearchClient>();
        services.AddTmdbClient<IPersonClient, PersonClient>();
        services.AddTmdbClient<IDiscoverClient, DiscoverClient>();
        services.AddSingleton<IImageUrlBuilder, ImageUrlBuilder>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddSingleton<ISlugGenerator, SlugGenerator>();
        services.AddScoped<NetworkSyncService>();
        services.AddScoped<SeasonSyncService>();
        services.AddScoped<EpisodeSyncService>();
        services.AddHttpContextAccessor();
        services.AddScoped<MovieImportService>();
        services.AddScoped<TvSeriesImportService>();
        services.AddScoped<IContentImportService, ContentImportService>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<GenreSyncService>();
        services.AddScoped<CompanySyncService>();
        services.AddScoped<CountrySyncService>();
        services.AddScoped<PersonSyncService>();
        services.AddScoped<ContentImportService>();

        return services;
    }
}