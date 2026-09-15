using Microsoft.EntityFrameworkCore;
using WatchBook.Domain.Features.Catalog.Entities;
using WatchBook.Infrastructure.External.TMDb.Responses.Common;
using WatchBook.Infrastructure.Persistence;

namespace WatchBook.Infrastructure.Features.Catalog.Shared.Services;

public sealed class CompanySyncService
{
    private readonly WatchBookDbContext _dbContext;

    public CompanySyncService(
        WatchBookDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<Company> SyncAsync(
        CompanyResponse response,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(response);


        var existingCompany = await _dbContext.Companies
            .FirstOrDefaultAsync(
                x => x.TmdbId == response.Id,
                cancellationToken);


        if (existingCompany is not null)
        {
            return existingCompany;
        }


        var company = new Company
        {
            TmdbId = response.Id,
            Name = response.Name,
            LogoPath = response.LogoPath,
            OriginCountry = response.OriginCountry,
            IsActive = true
        };

        await _dbContext.Companies.AddAsync(
            company,
            cancellationToken);

        return company;
    }
}