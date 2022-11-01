using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities;

public class Search : IDbEntity
{
    public Guid Id { get; set; }
    public Guid RelativeId { get; set; }
    public string Title { get; set; } = default!;
    public SearchCategory Category { get; set; }

    public Search() { }

    public Search(Guid relativeId, string title, SearchCategory category)
    {
        RelativeId = relativeId;
        Title = title;
        Category = category;
    }
}