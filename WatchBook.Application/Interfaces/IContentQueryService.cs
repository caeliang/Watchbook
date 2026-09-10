using WatchBook.Application.DTOs;

namespace WatchBook.Application.Interfaces;

public interface IContentQueryService
{
    Task<ContentDetailDto?> GetByIdAsync(
        int id,
        CancellationToken cancellationToken = default);
}