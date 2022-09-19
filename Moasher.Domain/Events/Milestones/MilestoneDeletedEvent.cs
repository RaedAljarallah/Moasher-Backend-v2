using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Milestones;

public class MilestoneDeletedEvent : DomainEvent
{
    public InitiativeMilestone Milestone { get; }

    public MilestoneDeletedEvent(InitiativeMilestone milestone)
    {
        Milestone = milestone;
    }
}