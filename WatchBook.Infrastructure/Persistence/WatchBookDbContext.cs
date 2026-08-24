using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Infrastructure.Identity;

namespace WatchBook.Infrastructure.Persistence;

/// <summary>
/// The unified database context for WatchBook, combining ASP.NET Core Identity
/// and all domain entities for catalog, relations, and user-generated content.
/// All entities share a single SQL Server database.
/// </summary>
public class WatchBookDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="WatchBookDbContext"/> class.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions{WatchBookDbContext}"/>.</param>
    public WatchBookDbContext(DbContextOptions<WatchBookDbContext> options)
        : base(options)
    {
    }

    #region Catalog DbSets

    /// <summary>
    /// Gets or sets the collection of content items (movies and TV series).
    /// </summary>
    public DbSet<Content> Contents { get; set; }

    /// <summary>
    /// Gets or sets the collection of seasons belonging to TV series.
    /// </summary>
    public DbSet<Season> Seasons { get; set; }

    /// <summary>
    /// Gets or sets the collection of episodes belonging to seasons.
    /// </summary>
    public DbSet<Episode> Episodes { get; set; }

    /// <summary>
    /// Gets or sets the collection of genres used to categorize content.
    /// </summary>
    public DbSet<Genre> Genres { get; set; }

    /// <summary>
    /// Gets or sets the collection of people involved in content production.
    /// </summary>
    public DbSet<Person> People { get; set; }

    /// <summary>
    /// Gets or sets the collection of production and distribution companies.
    /// </summary>
    public DbSet<Company> Companies { get; set; }

    /// <summary>
    /// Gets or sets the collection of production countries.
    /// </summary>
    public DbSet<Country> Countries { get; set; }

    /// <summary>
    /// Gets or sets the collection of television networks.
    /// </summary>
    public DbSet<Network> Networks { get; set; }

    #endregion

    #region Relation DbSets

    /// <summary>
    /// Gets or sets the collection of content-genre relationships.
    /// </summary>
    public DbSet<ContentGenre> ContentGenres { get; set; }

    /// <summary>
    /// Gets or sets the collection of content-person relationships.
    /// </summary>
    public DbSet<ContentPerson> ContentPeople { get; set; }

    /// <summary>
    /// Gets or sets the collection of content-company relationships.
    /// </summary>
    public DbSet<ContentCompany> ContentCompanies { get; set; }

    /// <summary>
    /// Gets or sets the collection of content-country relationships.
    /// </summary>
    public DbSet<ContentCountry> ContentCountries { get; set; }

    /// <summary>
    /// Gets or sets the collection of content-network relationships.
    /// </summary>
    public DbSet<ContentNetwork> ContentNetworks { get; set; }

    #endregion

    /// <summary>
    /// Configures the model for the database using Fluent API.
    /// Applies all entity type configurations from this assembly.
    /// </summary>
    /// <param name="modelBuilder">The model builder used to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Content>()
    .HasIndex(x => x.TmdbId)
    .IsUnique();

        modelBuilder.Entity<Season>()
            .HasIndex(x => x.TmdbId)
            .IsUnique();

        modelBuilder.Entity<Episode>()
            .HasIndex(x => x.TmdbId)
            .IsUnique();
        // Apply all IEntityTypeConfiguration<T> configurations from this assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WatchBookDbContext).Assembly);
    }
}
