using WatchBook.Domain.Entities.Catalog;

namespace WatchBook.Domain.Entities.Relations;

/// <summary>
/// Represents the many-to-many relationship between content and a production/distribution company.
/// This is a junction table with no additional payload.
/// </summary>
public class ContentCompany
{
    /// <summary>
    /// The foreign key referencing the content.
    /// </summary>
    public int ContentId { get; set; }

    /// <summary>
    /// The foreign key referencing the company.
    /// </summary>
    public int CompanyId { get; set; }

    /// <summary>
    /// The navigation property to the related content.
    /// </summary>
    public Content Content { get; set; } = null!;

    /// <summary>
    /// The navigation property to the related company.
    /// </summary>
    public Company Company { get; set; } = null!;
}