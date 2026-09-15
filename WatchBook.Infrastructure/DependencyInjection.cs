using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WatchBook.Application.Features.Authentication.Services;
using WatchBook.Application.Features.Catalog.Interfaces;
using WatchBook.Application.Features.UserContent.Services;
using WatchBook.Infrastructure.External.TMDb.Clients;
using WatchBook.Infrastructure.External.TMDb.Extensions;
using WatchBook.Infrastructure.External.TMDb.Handlers;
using WatchBook.Infrastructure.External.TMDb.Interfaces;
using WatchBook.Infrastructure.External.TMDb.Options;
using WatchBook.Infrastructure.Features.Authentication.Configurations;
using WatchBook.Infrastructure.Features.Authentication.Identity;
using WatchBook.Infrastructure.Features.Authentication.Services;
using WatchBook.Infrastructure.Features.Catalog.Movie.Interfaces;
using WatchBook.Infrastructure.Features.Catalog.Movie.Services;
using WatchBook.Infrastructure.Features.Catalog.Shared.Services;
using WatchBook.Infrastructure.Features.Catalog.TvSeries.Services;
using WatchBook.Infrastructure.Features.System.Interfaces;
using WatchBook.Infrastructure.Features.System.Services;
using WatchBook.Infrastructure.Features.UserContent.Services;
using WatchBook.Infrastructure.Persistence;

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

        // Catalog synchronization
        services.AddScoped<NetworkSyncService>();
        services.AddScoped<SeasonSyncService>();
        services.AddScoped<EpisodeSyncService>();
        services.AddScoped<GenreSyncService>();
        services.AddScoped<CompanySyncService>();
        services.AddScoped<CountrySyncService>();
        services.AddScoped<PersonSyncService>();
        services.AddScoped<ContentPersonSyncService>();

        // Import
        services.AddHttpContextAccessor();
        services.AddScoped<MovieImportService>();
        services.AddScoped<TvSeriesImportService>();
        services.AddScoped<IContentImportService, ContentImportService>();

        // Application services
        services.AddScoped<IContentQueryService, ContentQueryService>();
        services.AddScoped<IWatchlistService, WatchlistService>();

        // Current user
        services.AddScoped<ICurrentUserService, CurrentUserService>();

        // Auth
        services.AddScoped<IAuthService, AuthService>();


        services.AddScoped<IWatchlistService, WatchlistService>();

        services.AddScoped<IFavoriteService, FavoriteService>();

        services.AddScoped<IWatchStatusService, WatchStatusService>();

        services.AddScoped<IWatchHistoryService, WatchHistoryService>();

        services.AddScoped<IContentRatingService, ContentRatingService>();
        return services;
    }
}