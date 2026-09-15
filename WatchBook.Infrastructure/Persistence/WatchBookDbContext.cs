using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Domain.Entities.Relations;
using WatchBook.Domain.Entities.User;
using WatchBook.Infrastructure.Identity;

namespace WatchBook.Infrastructure.Persistence;

public class WatchBookDbContext
    : IdentityDbContext<ApplicationUser, ApplicationRole, string>
{
    public WatchBookDbContext(
        DbContextOptions<WatchBookDbContext> options)
        : base(options)
    {
    }

    public DbSet<Content> Contents { get; set; }
    public DbSet<Season> Seasons { get; set; }
    public DbSet<Episode> Episodes { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Person> People { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Network> Networks { get; set; }

    public DbSet<ContentGenre> ContentGenres { get; set; }
    public DbSet<ContentPerson> ContentPeople { get; set; }
    public DbSet<ContentCompany> ContentCompanies { get; set; }
    public DbSet<ContentCountry> ContentCountries { get; set; }
    public DbSet<ContentNetwork> ContentNetworks { get; set; }

    public DbSet<Watchlist> Watchlists { get; set; }
    public DbSet<Favorite> Favorites { get; set; }
    public DbSet<WatchStatus> WatchStatuses { get; set; }

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

        modelBuilder.Entity<Watchlist>()
            .HasIndex(x => new
            {
                x.UserId,
                x.ContentId
            })
            .IsUnique();

        modelBuilder.Entity<Watchlist>()
            .HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Favorite>()
            .HasIndex(x => new
            {
                x.UserId,
                x.ContentId
            })
            .IsUnique();

        modelBuilder.Entity<Favorite>()
            .HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WatchStatus>()
            .HasIndex(x => new
            {
                x.UserId,
                x.ContentId
            })
            .IsUnique();

        modelBuilder.Entity<WatchStatus>()
            .HasOne(x => x.Content)
            .WithMany()
            .HasForeignKey(x => x.ContentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(WatchBookDbContext).Assembly);
    }
}