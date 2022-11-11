using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.EditRequests;

namespace Moasher.Domain.Events.EditRequests;

public class EditRequestCreatedEvent : DomainEvent
{
    public EditRequest EditRequest { get; }

    public EditRequestCreatedEvent(EditRequest editRequest)
    {
        EditRequest = editRequest;
    }
}