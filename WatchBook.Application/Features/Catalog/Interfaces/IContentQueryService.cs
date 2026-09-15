using WatchBook.Application.Features.Catalog.DTOs;

namespace WatchBook.Application.Features.Catalog.Interfaces;

public interface IContentQueryService
{
    Task<ContentDetailDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}