using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;

namespace Moasher.Domain.Entities.EditRequests;

public class EditRequestSnapshot : IDbEntity
{
    public Guid Id { get; set; }
    public Guid ModelId { get; set; }
    public string ModelName { get; set; } = default!;
    public string TableName { get; set; } = default!;
    public string? OriginalValues { get; set; }
    public EditRequestType Type { get; set; }
    public EditRequest EditRequest { get; set; } = default!;
    public Guid EditRequestId { get; set; }
}