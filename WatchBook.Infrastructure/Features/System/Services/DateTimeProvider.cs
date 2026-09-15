using WatchBook.Infrastructure.Features.System.Interfaces;

namespace WatchBook.Infrastructure.Features.System.Services;

/// <summary>
/// Default implementation of <see cref="IDateTimeProvider"/>.
/// </summary>
public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow
        => DateTime.UtcNow;

    public DateTimeOffset UtcNowOffset
        => DateTimeOffset.UtcNow;

    public DateOnly Today
        => DateOnly.FromDateTime(DateTime.UtcNow);
}