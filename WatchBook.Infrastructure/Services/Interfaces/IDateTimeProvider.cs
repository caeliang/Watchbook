namespace WatchBook.Infrastructure.Services.Interfaces;

/// <summary>
/// Provides the current date and time.
/// </summary>
public interface IDateTimeProvider
{
    DateTime UtcNow { get; }

    DateTimeOffset UtcNowOffset { get; }

    DateOnly Today { get; }
}