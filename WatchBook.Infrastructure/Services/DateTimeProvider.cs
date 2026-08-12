using WatchBook.Infrastructure.Services.Interfaces;

namespace WatchBook.Infrastructure.Services;

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