using Moasher.Application.Common.Services;
using Moasher.Domain.Entities.EditRequests;
using Moasher.Domain.Enums;

namespace Moasher.Application.Features.EditRequests;

public record EditRequestDto
{
    private IEnumerable<string> _scopes = Enumerable.Empty<string>();
    public Guid Id { get; init; }
    public string Code { get; set; } = default!;

    public IEnumerable<string> Scopes
    {
        get => GetScopes();
        set => _scopes = value;
    }
    
    public EditRequestType Type { get; set; }
    public EditRequestStatus Status { get; set; }
    public DateTimeOffset RequestedAt { get; set; }
    public string RequestedBy { get; set; } = default!;
    
    private IEnumerable<string> GetScopes()
    {
        return _scopes.Select(AttributeServices.GetDisplayName<EditRequest>);
    }
}