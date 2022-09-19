using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Milestones;

public class MilestoneCreatedEvent : DomainEvent
{
    public InitiativeMilestone Milestone { get; }

    public MilestoneCreatedEvent(InitiativeMilestone milestone)
    {
        Milestone = milestone;
    }
}