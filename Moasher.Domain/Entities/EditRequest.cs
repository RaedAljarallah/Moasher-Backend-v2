using Moasher.Domain.Common.Interfaces;
using Moasher.Domain.Enums;
using Moasher.Domain.Types;

namespace Moasher.Domain.Entities;

public class EditRequest : IDbEntity
{
    public Guid Id { get; set; }
    public string Code { get; set; } = default!;
    public string Model { get; set; } = default!;
    public string CommandName { get; set; } = default!;
    public string? CurrentValues { get; set; }
    public string? OriginalValues { get; set; }
    public EditRequestType Type { get; set; }
    public EditRequestStatus Status { get; set; }
    public DateTimeOffset RequestedAt { get; set; }
    public string RequestedBy { get; set; } = default!;
    public string? ActionBy { get; set; }
    public DateTimeOffset? ActionAt { get; set; }

    public static EditRequest CreateRequest(string commandName)
    {
        return new EditRequest
        {
            Code = Guid.NewGuid().ToString(),
            CommandName = commandName,
            Type = EditRequestType.Create,
            Status = EditRequestStatus.Pending,
            RequestedAt = LocalDateTime.Now
        };
    }
}