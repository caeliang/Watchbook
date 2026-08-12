namespace WatchBook.Domain.Enums.Content;

/// <summary>
/// Represents the production/release status of a movie or TV series.
/// </summary>
public enum ProductionStatus
{
    Unknown = 0,

    Rumored = 1,

    Planned = 2,

    InProduction = 3,

    PostProduction = 4,

    Released = 5,

    Canceled = 6
}