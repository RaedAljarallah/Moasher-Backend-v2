using Moasher.Domain.Enums;

namespace Moasher.Application.Features.SearchRecords;

public record SearchRecordDto
{
    public Guid Id { get; set; }
    public Guid RelativeId { get; set; }
    public string Title { get; set; } = default!;
    public SearchCategory Category { get; set; }
}