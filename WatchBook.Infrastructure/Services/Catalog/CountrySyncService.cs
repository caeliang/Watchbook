using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Entities.Catalog;
using WatchBook.Infrastructure.External.TMDb.Responses.Common;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Services.Catalog;

public sealed class CountrySyncService
{
    private readonly WatchBookDbContext _dbContext;

    public CountrySyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Country> SyncAsync(
        CountryResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);


        var existingCountry = await _dbContext.Countries
            .FirstOrDefaultAsync(
                x => x.Code == response.Code,
                cancellationToken);


        if (existingCountry is not null)
        {
            return existingCountry;
        }


        var country = new Country
        {
            Code = response.Code,
            Name = response.Name
        };


        await _dbContext.Countries.AddAsync(
            country,
            cancellationToken);


        await _dbContext.SaveChangesAsync(
            cancellationToken);


        return country;
    }
}