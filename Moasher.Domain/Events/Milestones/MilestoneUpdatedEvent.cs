using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Milestones;

public class MilestoneUpdatedEvent : DomainEvent
{
    public InitiativeMilestone Milestone { get; }

    public MilestoneUpdatedEvent(InitiativeMilestone milestone)
    {
        Milestone = milestone;
    }
}