using MediatR;
using Moasher.Domain.Entities.EditRequests;

namespace Moasher.Application.Features.EditRequests.Commands.RejectEditRequest;

public record RejectEditRequestCommand : IRequest<EditRequest>
{
    private string? _justification;
    public Guid Id { get; set; }

    public string? Justification { get => _justification; set => _justification = value?.Trim(); }
}