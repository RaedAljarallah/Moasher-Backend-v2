using Moasher.Domain.Common.Abstracts;
using Moasher.Domain.Entities.InitiativeEntities;

namespace Moasher.Domain.Events.Projects;

public class ProjectPhaseCompletedEvent : DomainEvent
{
    public InitiativeProject Project { get; }

    public ProjectPhaseCompletedEvent(InitiativeProject project)
    {
        Project = project;
    }
}